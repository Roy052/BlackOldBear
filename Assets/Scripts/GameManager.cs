using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float speed = 5.0f; // 1 ~ 10.  1 - 생성으로부터 도착까지 2초 / 10 - 생성으로부터 도착까지 0.2초
                               // y = -0.2x + 2.2 when x = speed
    public float time;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void MenuToBattle()
    {
        SceneManager.LoadScene(1);
    }
}
