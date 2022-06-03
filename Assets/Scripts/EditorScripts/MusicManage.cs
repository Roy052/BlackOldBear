using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManage : MonoBehaviour
{
    [HideInInspector]
    public AudioSource mSource;

    public GameObject lineManagerObj;
    LineManageScript lineManageScript;
    public bool playStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        mSource = GetComponent<AudioSource>();
        mSource.clip = null;
        mSource.time = 0;
        lineManageScript = lineManagerObj.GetComponent<LineManageScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMusic(AudioClip mClip)
    {
        mSource.clip = mClip;
        mSource.time = 0;
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

                mSource.time = playPoint;
                mSource.Play();
                lineManageScript.musicPlay();
                playStatus = true;
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
