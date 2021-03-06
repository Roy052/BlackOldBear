using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int languageType = 0;
    public int score = 0;
    [Range(1.0f, 10.0f)]
    public float speed = 5.0f; // 1 ~ 10.  1 - 생성으로부터 도착까지 1.6초 / 10 - 생성으로부터 도착까지 0.17초
                               // y = -0.159x + 1.759 when x = speed
    public float time;
    public float musicLoadDelay = 3.0f;
    public float musicEndDelay = 3.0f;
    public string patternName;
    public float perfectSize = 1; // float
    public int frequencyChange = 0;

    public static float FadeTimeGap = 0.5f;
    public bool inBoss = false;

    public int stageNum;
    //Unique GameManager
    private static GameManager gameManagerInstance;
    FadeManager fadeManager;
    PauseMenuManager pauseMenuManager;
    UIBarManager uIBarManager;
    bool pauseON = false;

    public List<string> patternList = new List<string>();
    public List<string> bossPatternList = new List<string>();

    // for debug
    // public readonly string[] patternList = { "test_1" };    
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);

        //Data Load
        var info = new DirectoryInfo("Assets/Patterns");
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if(file.Name.Substring(file.Name.Length - 5, 5) != ".meta")
            {
                patternList.Add(file.Name.Substring(0, file.Name.Length - 5));
            }
        }

        info = new DirectoryInfo("Assets/BossPatterns");
        fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Name.Substring(file.Name.Length - 5, 5) != ".meta")
            {
                bossPatternList.Add(file.Name.Substring(0, file.Name.Length - 5));
            }
        }

        stageNum = 0;
        fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        StartCoroutine(fadeManager.FadeIn(1));

        pauseMenuManager = GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>();
        uIBarManager = this.GetComponent<UIBarManager>();
        uIBarManager.UIBarOFF();

        pauseMenuManager.PauseOFF();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseON == true)
                PauseMenuOFF();
            else
                PauseMenuON();

            pauseON = !pauseON;
        }
    }

    public void MenuToBattle()
    {
        SceneManager.LoadScene("MapScene");
        uIBarManager.UIBarON();
    }

    public void GameOver()
    {
        this.GetComponent<MapRecorder>().recorded = false;
        this.GetComponent<StatusManager>().ResetHealth();
        this.GetComponent<ItemManager>().ResetMoneyandItem();
        this.GetComponent<Accessory_Manager>().AccessoryReset();
        SceneManager.LoadScene("Menu");
        uIBarManager.UIBarOFF();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void PauseMenuON()
    {
        pauseMenuManager.PauseON();
    }

    public void PauseMenuOFF()
    {
        pauseMenuManager.PauseOFF();
    }

    public void ToPattern()
    {
        SceneManager.LoadScene("NoteEditor");
    }

    

    public void UIBarON()
    {
        uIBarManager.UIBarON();
    }

    public void UIBarOFF()
    {
        uIBarManager.UIBarOFF();
    }

    public void AccessoryON()
    {
        this.GetComponent<Accessory_Manager>().AccessoryON();
    }

    public void AccessoryOFF()
    {
        this.GetComponent<Accessory_Manager>().AccessoryOFF();
    }

    public void BattleToMenu()
    {
        SceneManager.LoadScene("Menu");
        uIBarManager.UIBarOFF();
    }

    public void LanguageChange(int val)
    {
        languageType = val;

        //Menu Button Change
        GameObject temp = GameObject.Find("MenuButtons");
        if (temp != null)
        {
            temp.GetComponent<MenuButton>().LanguageChange(languageType);
        }

        //Accessory Change
        this.GetComponent<Accessory_Manager>().LanguageChange(languageType);

        //SceneChange
        temp = GameObject.FindGameObjectWithTag("SceneManager");
        if (temp != null)
        {
            temp.GetComponent<SceneByScene>().LanguageChange(languageType);

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 3:
                    temp.GetComponent<StartManager>().LanguageChange(languageType);
                    break;
                case 4:
                    temp.GetComponent<ShopManager>().LanguageChange(languageType);
                    break;
                case 5:
                    temp.GetComponent<BornfireManager>().LanguageChange(languageType);
                    break;
                case 6:
                    temp.GetComponent<EventManager>().LanguageChange(languageType);
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
            }
        }
    }
}
