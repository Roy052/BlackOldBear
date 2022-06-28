using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public bool chestON;

    RewardManager rewardManager;
    GameObject gameManagerObject;

    private void Start()
    {
        chestON = false;
        gameManagerObject = GameObject.Find("GameManager");
        rewardManager = this.gameObject.GetComponent<RewardManager>();

        this.gameObject.GetComponent<SceneByScene>().NextButtonON();
    }

    public void ChestON()
    {
        chestON = true;
        rewardManager.RewardON();
    }

    public void ChestOFF()
    {
        chestON = false;
        rewardManager.RewardOFF();
    }
}
