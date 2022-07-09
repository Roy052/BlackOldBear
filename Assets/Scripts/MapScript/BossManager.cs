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
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        rewardManager = this.gameObject.GetComponent<RewardManager>();

        gm = gameManagerObject.GetComponent<GameManager>();
        gm.time = 0;
        gm.patternName = gm.patternList[Random.Range(0, gm.patternList.Length)];

        gm.UIBarOFF();
        SceneManager.LoadSceneAsync("BossBattle", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (battleManager == null)
        {
            GameObject temp = GameObject.Find("BattleManager");
            if (temp != null)
                battleManager = temp.GetComponent<BattleManager>();
            WolfManager wolfManager = GameObject.Find("Wolfs").GetComponent<WolfManager>();
            for (int i = 0; i < 500; i++)
                wolfManager.wolfHp[i] = 999;
        }
        else if (onetime == false)
            gameEnd = battleManager.gameEnd;
        if (onetime == false && gameEnd == true && battleManager != null)
        {
            this.gameObject.GetComponent<SceneByScene>().NextButtonON();
            rewardManager.ratio = battleManager.scoreRatio;
            onetime = true;
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
