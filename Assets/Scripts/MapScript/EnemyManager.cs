using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    RewardManager rewardManager;
    GameObject gameManagerObject;
    bool gameEnd = true;
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        rewardManager = this.gameObject.GetComponent<RewardManager>();
    }

    private void Update()
    {
        if(gameEnd == true)
            this.gameObject.GetComponent<SceneByScene>().NextButtonON();
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
