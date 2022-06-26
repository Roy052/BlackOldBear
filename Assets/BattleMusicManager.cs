using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusicManager : MonoBehaviour
{
    public string musicName = "Electronic_2";
    PatternData2 pData;
    public List<WolfData2> wData;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        pData = SaveScript.loadData2(musicName);
        //Debug.Log(pData.BGM);
        //Debug.Log(pData.difficult);
        wData = pData.wolfs;
        //Debug.Log(wData.Count);
    
        audioSource = gameObject.GetComponent<AudioSource>();
        // audioSource.clip
        audioSource.mute = false;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
