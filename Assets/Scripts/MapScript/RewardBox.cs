using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBox : MonoBehaviour
{
    public int num;
    public RewardManager rewardManager;
    private void OnMouseDown()
    {
        rewardManager.GainReward(num);
    }
}
