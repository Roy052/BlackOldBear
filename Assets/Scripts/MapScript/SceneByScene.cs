using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByScene : MonoBehaviour
{
    public GameObject nextButton;

    private void Start()
    {
        nextButton.SetActive(false);
    }
    public void NextButtonON()
    {
        nextButton.SetActive(true);
    }

    public void NextButtonPushed()
    {
        SceneManager.LoadScene("MapScene");
    }
}
