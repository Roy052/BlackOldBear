using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    [SerializeField] Sprite playSP, pauseSP;
    public GameObject lineManageObj, musicManagerObj;

    LineManageScript lineManageScript;
    MusicManage musicManageScript;
    SpriteRenderer SPrenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineManageScript = lineManageObj.GetComponent<LineManageScript>();
        musicManageScript = musicManagerObj.GetComponent<MusicManage>();
        SPrenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlaySprite()
    {
        SPrenderer.sprite = playSP;
    }

    private void OnMouseDown()
    {
        if (musicManageScript.playStatus)
        {
            musicManageScript.musicPause();
            SPrenderer.sprite = playSP;
        }
        else
        {
            musicManageScript.musicPlay();
            SPrenderer.sprite = pauseSP;
        }
    }
}
