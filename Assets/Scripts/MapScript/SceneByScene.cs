using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByScene : MonoBehaviour
{
    public GameObject nextButton;
    public RewardManager rewardManager;
    private void Start()
    {
        nextButton.SetActive(false);
        rewardManager = this.GetComponent<RewardManager>();
    }
    public void NextButtonON()
    {
        nextButton.SetActive(true);
    }

    public void NextButtonPushed()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void RewardON()
    {
        rewardManager.RewardON();
    }

    public void RewardOFF()
    {
        rewardManager.RewardOFF();
    }
}
