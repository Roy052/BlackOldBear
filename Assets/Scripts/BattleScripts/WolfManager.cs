using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
    // public string patternName = "Electronic_2";
    public List<WolfData2> wData;

    public GameObject bear;
    public GameObject wolf;
    public GameObject fox;
    public GameObject EmptyWolf;
    Vector3 position;
    public List<GameObject> wolfGenerated = new List<GameObject>();
    public List<float> wolfTyped = new List<float>();
    public List<GameObject> wolfEmpty = new List<GameObject>();
    float time_start;
    public float time_current = 0f;
    public float radius = 7f;
    public float curveAngle = 70;
    public int freq = 5;
    GameManager gm;
    bool noteAvailable = true;

    float type = 0.0f;
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
    [HideInInspector] public int now = 0;
    public int emptyFirst;
    [HideInInspector] public int[] emptyState = new int[500];
    [HideInInspector] public int[] wolfHp = new int[500];
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        musicLoadDelay = gm.musicLoadDelay;
        time_start = Time.time;
        position = bear.transform.position;

        // pData = SaveScript.loadData2(patternName);
        // wData = pData.wolfs;

        maxNote = wData.Count;

        type = wData[noteCount].type;
        nextGenTime = wData[noteCount].time;
        nextAngle = wData[noteCount++].angle;
        nextGenPos = AngleToPosition(nextAngle);
        //Debug.Log("time: "+nextGenTime+" / angle: "+nextAngle+" / pos: "+nextGenPos);

        for(int i = 0; i < maxNote; i++)
        {
            wolfHp[i] = 1;
            emptyState[i] = 0;
        }
        
        for(int i = maxNote - 1; i >= 1; i--)
        {
            if (wData[i].time - wData[i - 1].time < 1.1f)
            {
                if(Random.Range(0, freq) == 0)
                {
                    wolfHp[i - 1] += wolfHp[i];
                    emptyState[i] = 1;
                }
            }
        }
        emptyFirst = -1;
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

        if (time_current>=nextGenTime+musicLoadDelay-(-0.159 * gm.speed + 1.759))
        {
            if(noteAvailable)
            {
                if (type == 0)
                {
                    wolfTyped.Add(0);
                    if(emptyState[noteCount-1]==0)
                        newNote = Instantiate(wolf, nextGenPos, Quaternion.identity);
                    else
                        newNote = Instantiate(EmptyWolf, nextGenPos, Quaternion.identity);
                }
                else if (type == 1)
                {
                    wolfTyped.Add(0);
                    if (emptyState[noteCount-1] == 0)
                        newNote = Instantiate(wolf, nextGenPos*2, Quaternion.identity);
                    else
                        newNote = Instantiate(EmptyWolf, nextGenPos*2, Quaternion.identity);
                }
                else if (type == 2)
                {
                    wolfTyped.Add(1);
                    if (emptyState[noteCount-1] == 0)
                        newNote = Instantiate(fox, Quaternion.Euler(0, 0, Mathf.Rad2Deg * 1.312235f) * nextGenPos, Quaternion.identity);
                    else
                        newNote = Instantiate(EmptyWolf, Quaternion.Euler(0, 0, Mathf.Rad2Deg * 1.312235f) * nextGenPos, Quaternion.identity);
                }
                else
                {
                    wolfTyped.Add(1);
                    if (emptyState[noteCount-1] == 0)
                        newNote = Instantiate(fox, Quaternion.Euler(0, 0, Mathf.Rad2Deg * 1.312235f) * nextGenPos * 2, Quaternion.identity);
                    else
                        newNote = Instantiate(EmptyWolf, Quaternion.Euler(0, 0, Mathf.Rad2Deg * 1.312235f) * nextGenPos*2, Quaternion.identity);
                }

                //newNote = Instantiate(wolf, nextGenPos, Quaternion.identity);
                if (emptyState[noteCount-1] == 0)
                    wolfGenerated.Add(newNote);
                else
                {
                    wolfEmpty.Add(newNote);
                }
                if(!isNextExist)
                {
                    noteAvailable = false;
                }
            }

            if(isNextExist)
            {
                type = wData[noteCount].type;
                nextGenTime = wData[noteCount].time;
                nextAngle = wData[noteCount++].angle;
                nextGenPos = nextGenPos = AngleToPosition(nextAngle);
                //Debug.Log("time: "+nextGenTime+" / angle: "+nextAngle+" / pos: "+nextGenPos);
            }
            if(noteCount == maxNote)
                isNextExist = false;
            
        }
    }
}
