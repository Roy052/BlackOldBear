using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBox : MonoBehaviour
{
    public int num;
    RewardManager rewardManager;

    private void Start()
    {
        rewardManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<RewardManager>();
    }
    private void OnMouseDown()
    {
        rewardManager.GainReward(num);
    }
}
