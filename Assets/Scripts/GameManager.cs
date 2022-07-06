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
    }

    public void MenuToBattle()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Menu");
    }

}
