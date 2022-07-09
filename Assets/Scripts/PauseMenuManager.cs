using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    GameObject gameManagerObject;
    [SerializeField] Slider volumeSlider;

    public Text volume, language;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");

        if (gameManagerObject.GetComponent<GameManager>().languageType == 0)
        {
            volume.text = "Volume";
            language.text = "Select Language";
        }
        else
        {
            volume.text = "볼륨";
            language.text = "언어 설정";
        }
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


    public void ToMenu()
    {
        gameManagerObject.GetComponent<GameManager>().BattleToMenu();
        PauseOFF();
    }

    public void LanguageChange(int val)
    {
        gameManagerObject.GetComponent<GameManager>().LanguageChange(val);
        if(val == 0)
        {
            volume.text = "Volume";
            language.text = "Select Language";
        }
        else
        {
            volume.text = "볼륨";
            language.text = "언어 설정";
        }
    }
}
