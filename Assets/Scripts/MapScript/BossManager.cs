using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    RewardManager rewardManager;
    GameObject gameManagerObject;
    GameManager gm;
    bool gameEnd = false;
    BattleManager battleManager;
    bool onetime = false;
    WolfManager wolfManager;

    int temp;
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        rewardManager = this.gameObject.GetComponent<RewardManager>();

        gm = gameManagerObject.GetComponent<GameManager>();
        gm.time = 0;
        gm.patternName = gm.bossPatternList[Random.Range(0, gm.bossPatternList.Count)];

        gm.UIBarOFF();
        gm.AccessoryOFF();

        //보스전 기믹용
        temp = gm.frequencyChange;
        gm.frequencyChange = -1;
        gm.inBoss = true;

        SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (battleManager == null)
        {
            GameObject temp = GameObject.Find("BattleManager");
            if (temp != null)
                battleManager = temp.GetComponent<BattleManager>();
           
        }
        else if (onetime == false)
            gameEnd = battleManager.gameEnd;

        if(wolfManager == null)
        {
            GameObject temp = GameObject.Find("Wolfs");
            if (temp != null)
                wolfManager = temp.GetComponent<WolfManager>();
        }

        if (onetime == false && gameEnd == true && battleManager != null)
        {
            this.gameObject.GetComponent<SceneByScene>().NextButtonON();
            rewardManager.ratio = battleManager.scoreRatio;
            onetime = true;
            RewardON();
            gm.UIBarON();
            gm.AccessoryON();

            //보스전 기믹 제거
            gm.frequencyChange = temp;
            gm.inBoss = false;
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
