using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public string patternName;
    public string musicName;
    [HideInInspector] public PatternData2 pData;

    WolfManager wm;
    BattleMusicManager bmm;
    GameManager gm;
    public GameObject Mouse;
    public GameObject Center;
    public GameObject Camera;
    public float musicDuration;
    public bool gameEnd = false;

    public float anglePf = 0.97f;
    public float angleGr = 0.85f;

    public int scPP = 10;
    public int scPG = 7;
    public int scPB = 1;
    public int scGP = 5;
    public int scGG = 3;
    public int scGB = -1;
    public int scB = -10;

    int noteCount;
    int perfectCount = 0;
    int greatCount = 0;
    int badCount = 0;


    float musicLoadDelay;
    float musicEndDelay;
    public float scoreRatio;
    void Start()
    {
        wm = GameObject.Find("Wolfs").GetComponent<WolfManager>();
        bmm = GameObject.Find("BattleManager").GetComponent<BattleMusicManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        patternName = gm.patternName;
        pData = SaveScript.loadData2(patternName);
        wm.wData = pData.wolfs;
        musicName = pData.BGM;
        bmm.musicName = musicName;

        musicLoadDelay = gm.musicLoadDelay;
        musicEndDelay = gm.musicEndDelay;
        noteCount = pData.wolfs.Count;
        Debug.Log(noteCount);
    }

    void Update()
    {
        if(gm.time>= musicLoadDelay+musicEndDelay+musicDuration && gameEnd == false)
        {
            gameEnd = true;
            scoreRatio = getScoreRatio();
            Debug.Log(scoreRatio);
            Mouse.SetActive(false);
            Center.SetActive(false);
            Camera.SetActive(false);
            if(GameObject.Find("GameManager").GetComponent<Accessory_Manager>().IsAccessoryOwn(0) == true)
            {
                GameObject.Find("GameManager").GetComponent<StatusManager>().ChangeHealth(2);
            }
        }
    }

    public void countJudge(int state)
    {
        switch(state)
        {
            case 1:
                badCount++;
                break;
            case 2:
                greatCount++;
                break;
            case 3:
                perfectCount++;
                break;
            default:
                break;
        }
    }

    public float getScoreRatio()
    {
        if((greatCount+badCount) == 0)
            return 1;
        return (float)perfectCount / noteCount;
    }
}
