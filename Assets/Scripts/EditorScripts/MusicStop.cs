using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStop : MonoBehaviour
{
    public GameObject lineManageObj, musicManagerObj, startObj;
    LineManageScript lineManageScript;
    MusicManage musicManageScript;
    MusicStart startScript;

    // Start is called before the first frame update
    void Start()
    {
        lineManageScript = lineManageObj.GetComponent<LineManageScript>();
        musicManageScript = musicManagerObj.GetComponent<MusicManage>();
        startScript = startObj.GetComponent<MusicStart>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        musicManageScript.musicStop();
        startScript.setPlaySprite();
    }
}
