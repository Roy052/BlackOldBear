using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{

    RewardManager rewardManager;
    GameObject gameManagerObject;
    GameManager gm;
    bool gameEnd = false;
    BattleManager battleManager;
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        rewardManager = this.gameObject.GetComponent<RewardManager>();
        
        gm = gameManagerObject.GetComponent<GameManager>();

        gm.patternName = gm.patternList[Random.Range(0, gm.patternList.Length)];
        
        gm.UIBarOFF();
        SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (battleManager == null)
        {
            GameObject temp = GameObject.Find("BattleManager");
            if(temp != null)
                battleManager = temp.GetComponent<BattleManager>();
        }
        else
            gameEnd = battleManager.gameEnd;
        if(gameEnd == true)
        {
            this.gameObject.GetComponent<SceneByScene>().NextButtonON();
            rewardManager.ratio = battleManager.scoreRatio;
            RewardON();
            gm.UIBarON();
        }
            
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
