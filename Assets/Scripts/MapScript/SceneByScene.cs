using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByScene : MonoBehaviour
{
    public GameObject nextButton;
    public RewardManager rewardManager;
    public Sprite[] nextButtonImages;

    int languageType = 0;
    bool ready = true, onetime = false;
    FadeManager fadeManager;
    private void Start()
    {
        nextButton.SetActive(false);
        fadeManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();
        rewardManager = this.GetComponent<RewardManager>();
        LanguageChange(GameObject.Find("GameManager").GetComponent<GameManager>().languageType);
    }

    private void Update()
    {
        if (onetime == false && ready == true)
        {
            StartCoroutine(fadeManager.FadeIn(GameManager.FadeTimeGap));
            onetime = true;
        }
    }
    public void NextButtonON()
    {
        nextButton.SetActive(true);
    }

    public void NextButtonPushed()
    {
        StartCoroutine(LoadSceneWithTerm(GameManager.FadeTimeGap));
    }

    public void RewardON()
    {
        GameObject.Find("RewardCanvas").GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rewardManager.RewardON();
    }

    public void RewardOFF()
    {
        rewardManager.RewardOFF();
    }

    IEnumerator LoadSceneWithTerm(float term)
    {
        StartCoroutine(fadeManager.FadeOut(term));
        yield return new WaitForSeconds(term);
        if(SceneManager.GetActiveScene().name == "Enemy")
        {
            SceneManager.UnloadSceneAsync("Battle");
        }
        SceneManager.LoadScene("MapScene");
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        nextButton.GetComponent<SpriteRenderer>().sprite = nextButtonImages[languageType];
    }
}
