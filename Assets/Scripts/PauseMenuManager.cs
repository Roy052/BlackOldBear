using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    GameObject gameManagerObject;
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void PauseON()
    {
        this.gameObject.SetActive(true);
    }

    public void PauseOFF()
    {
        this.gameObject.SetActive(false);
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }

}
