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

    public static float FadeTimeGap = 0.5f;

    public int stageNum;
    //Unique GameManager
    private static GameManager gameManagerInstance;
    FadeManager fadeManager;
    PauseMenuManager pauseMenuManager;
    UIBarManager uIBarManager;
    bool pauseON = false;
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
        SceneManager.LoadScene("Menu");
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
