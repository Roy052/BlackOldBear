using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField] Sprite SPon, SPoff;
    [SerializeField] GameObject musicObj;
    MusicManage musicScript;

    AudioSource mSource;
    SpriteRenderer SPrenderer;
    bool isOn = true;
    float BPM;
    float baseOffset;
    float nextPos;

    // Start is called before the first frame update
    void Start()
    {
        mSource = GetComponent<AudioSource>();
        SPrenderer = GetComponent<SpriteRenderer>();
        musicScript = musicObj.GetComponent<MusicManage>();
    }

    public void tik()
    {
        mSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicScript && musicScript.playStatus)
        {
            if (nextPos < musicScript.mSource.time)
            {
                if (isOn)
                    tik();

                getNextPos();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isOn)
        {
            SPrenderer.sprite = SPoff;
            isOn = false;
        }
        else
        {
            SPrenderer.sprite = SPon;
            isOn = true;
        }
    }

    public void metronomePlay()
    {
        nextPos = (baseOffset / 1000) + (60f / BPM);
    }

    public void getNextPos()
    {
        if (musicScript && musicScript.playStatus)
        {
            float currentPos = musicScript.mSource.time;
            float bo = baseOffset / 1000;
            float gap = 60f / BPM;
            nextPos = Mathf.Floor((currentPos - bo) / gap + 1) * gap + bo;
        }
    }

    public void setBPM(float bpm)
    {
        BPM = bpm;
        getNextPos();
    }

    public void setBaseOffset(float bo)
    {
        baseOffset = bo;
        getNextPos();
    }
}
