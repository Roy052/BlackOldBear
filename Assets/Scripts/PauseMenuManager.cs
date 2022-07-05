using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    GameObject gameManagerObject;
    public AudioSource bgmAudioSource, effectAudioSource, eventEffectAudioSource;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        this.gameObject.SetActive(false);
    }

    public void PauseON()
    {
        this.gameObject.SetActive(false);
    }

    public void PauseOFF()
    {
        this.gameObject.SetActive(false);
    }
}
