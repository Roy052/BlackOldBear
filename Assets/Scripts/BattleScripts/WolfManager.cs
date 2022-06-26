using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
    public string musicName = "Electronic_2";
    PatternData2 pData;
    public List<WolfData2> wData;

    public GameObject bear;
    public GameObject wolf;
    Vector3 position;
    public List<GameObject> wolfGenerated = new List<GameObject>();
    float time_start;
    public float time_current = 0f;
    public float radius = 7f;
    GameManager gm;
    bool noteAvailable = true;

    int noteCount = 0;
    int maxNote;
    bool isNextExist = true;
    float nextGenTime = 0.0f;
    float nextAngle = 0.0f;
    float musicLoadDelay = 3.0f;
    Vector3 nextGenPos;
    GameObject newNote;
    [HideInInspector] public int first = 0;
    [HideInInspector] public bool clicked = false;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        musicLoadDelay = gm.musicLoadDelay;
        time_start = Time.time;
        position = bear.transform.position;

        pData = SaveScript.loadData2(musicName);
        wData = pData.wolfs;

        maxNote = wData.Count;

        nextGenTime = wData[noteCount].time;
        nextAngle = wData[noteCount++].angle;
        nextGenPos = AngleToPosition(nextAngle);
        Debug.Log("time: "+nextGenTime+" / angle: "+nextAngle+" / pos: "+nextGenPos);
    }

    public Vector3 AngleToPosition(float Angle)
    {
        float x = radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
        return new Vector3(x, y, 0);
    }

    public Vector3 getBearPosition()
    {
        return position;
    }

    private void Update()
    {
        time_current = Time.time - time_start;
        gm.time = time_current;

        if(time_current>=nextGenTime+musicLoadDelay-(-0.2*gm.speed + 2.2))
        {
            if(noteAvailable)
            {
                newNote = Instantiate(wolf, nextGenPos, Quaternion.identity);
                wolfGenerated.Add(newNote);
                if(!isNextExist)
                {
                    noteAvailable = false;
                }
            }

            if(isNextExist)
            {
                nextGenTime = wData[noteCount].time;
                nextAngle = wData[noteCount++].angle;
                nextGenPos = nextGenPos = AngleToPosition(nextAngle);
                Debug.Log("time: "+nextGenTime+" / angle: "+nextAngle+" / pos: "+nextGenPos);
            }
            if(noteCount == maxNote)
                isNextExist = false;
            
        }
    }
}
