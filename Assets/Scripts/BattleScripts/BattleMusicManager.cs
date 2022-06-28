using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusicManager : MonoBehaviour
{
    public string musicName = "Electronic_2";
    GameManager gm;
    float musicLoadDelay = 3.0f;
    public AudioSource audioSource;
    bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = this.GetComponent<AudioSource>();
        musicLoadDelay = gm.musicLoadDelay;

        audioSource.clip = Resources.Load<AudioClip>("EditorResourse/Electronic");
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.time>=musicLoadDelay && !playing)
        {
            audioSource.Play();
            playing = true;
        }
    }
}
