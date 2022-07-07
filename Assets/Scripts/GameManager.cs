using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    [Range(1.0f, 10.0f)]
    public float speed = 5.0f; // 1 ~ 10.  1 - 생성으로부터 도착까지 1.6초 / 10 - 생성으로부터 도착까지 0.17초
                               // y = -0.159x + 1.759 when x = speed
    public float time;
    public float musicLoadDelay = 3.0f;
    public float musicEndDelay = 3.0f;
    public string patternName;
    public int perfectSize = 2; // 0 - 4

    public static float FadeTimeGap = 0.5f;

    public int stageNum;
    //Unique GameManager
    private static GameManager gameManagerInstance;
    FadeManager fadeManager;
    PauseMenuManager pauseMenuManager;
    UIBarManager uIBarManager;
    bool pauseON = false;

    public readonly string[] patternList = { "abstract_1", "ambient_1", 
        "best_time_1", "Boom_Bap_Hip_Easy", "Chill_1", "Coding_night", "Electronic_2", 
        "Fluidity_1","for_food_1", "spirit_1" };

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
            if(pauseON == true)
                pauseMenuManager.PauseOFF();
            else
                pauseMenuManager.PauseON();

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

    public void UIBarON()
    {
        uIBarManager.UIBarON();
    }

    public void UIBarOFF()
    {
        uIBarManager.UIBarOFF();
    }
}
