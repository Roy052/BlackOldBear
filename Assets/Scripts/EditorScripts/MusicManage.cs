using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManage : MonoBehaviour
{
    [HideInInspector]
    public AudioSource mSource;

    public GameObject lineManagerObj, metronomeObj;
    LineManageScript lineManageScript;
    Metronome metronomeScript;
    public bool playStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        mSource = GetComponent<AudioSource>();
        mSource.clip = null;
        mSource.time = 0;
        lineManageScript = lineManagerObj.GetComponent<LineManageScript>();
        metronomeScript = metronomeObj.GetComponent<Metronome>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMusic(AudioClip mClip)
    {
        Debug.Log("MusicManage.setMusic");
        mSource.clip = mClip;
        mSource.time = 0;
        lineManageScript.musicLength = mClip.length;
    }

    public void musicPlay()
    {
        if (mSource.clip != null)
        {
            if (playStatus == false)
            {
                // 재생 시점
                float playPoint = lineManageScript.currentPos;
                if (playPoint < 0)
                    playPoint = 0;
                else if (playPoint > mSource.clip.length)
                    playPoint = 0;

                Debug.Log(playPoint + " / " + mSource.clip.length);
                mSource.time = playPoint;
                mSource.Play();
                lineManageScript.musicPlay();
                playStatus = true;
                metronomeScript.getNextPos();
            }
            else
            {
                mSource.Pause();
                playStatus = false;
            }
        }
    }

    public void musicPause()
    {
        mSource.Pause();
        lineManageScript.musicPause();
        playStatus = false;
    }

    public void musicStop()
    {
        mSource.Stop();
        lineManageScript.musicStop();
        playStatus = false;
        lineManageScript.currentPos = 0;
        lineManageScript.circleReload();
    }
}
