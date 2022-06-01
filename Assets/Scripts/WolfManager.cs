using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
    [System.Serializable]
    public struct wolfDat
    {
        public Vector3 genPos;
        public float arriveTime;
    }

    public GameObject bear;
    public GameObject wolf;
    Vector3 position;
    public List<wolfDat> wolfList = new List<wolfDat>();
    public List<GameObject> wolfGenerated = new List<GameObject>();
    float time_start;
    public float time_current = 0f;

    GameManager gm;
    bool noteAvailable = true;

    int noteCount = 0;
    int maxNote;
    bool isNextExist = true;
    float nextGenTime = 0.0f;
    Vector3 nextGenPos;
    GameObject newNote;
    [HideInInspector] public int first = 0;
    [HideInInspector] public bool clicked = false;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        time_start = Time.time;
        position = bear.transform.position;

        maxNote = wolfList.Count;
        nextGenTime = wolfList[noteCount].arriveTime;
        nextGenPos = wolfList[noteCount++].genPos;
    }

    public Vector3 getBearPosition()
    {
        return position;
    }

    private void Update()
    {
        time_current = Time.time - time_start;
        gm.time = time_current;

        if(time_current>=nextGenTime)
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
                nextGenTime = wolfList[noteCount].arriveTime;
                nextGenPos = wolfList[noteCount++].genPos;
            }
            if(noteCount == maxNote)
                isNextExist = false;
            
        }
    }
}
