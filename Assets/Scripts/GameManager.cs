using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float speed = 5.0f;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void MenuToBattle()
    {
        SceneManager.LoadScene(1);
    }
}
