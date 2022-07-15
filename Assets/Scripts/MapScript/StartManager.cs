using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject background;
    public Sprite[] backgroundImages;
    public Sprite[] backgroundImages_eng;
    public Button nextBeforeButton;
    public Text buttonText;
    GameManager gm;
    int languageType = 0;
    int pageNum = 0;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.languageType == 0)
            buttonText.text = "Next";
        else
            buttonText.text = "다음";
        nextBeforeButton.onClick.AddListener(NextDesc);
        LanguageChange(gm.languageType);
        this.GetComponent<SceneByScene>().NextButtonON();
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        if(languageType == 0)
            background.GetComponent<SpriteRenderer>().sprite = backgroundImages_eng[pageNum];
        else
            background.GetComponent<SpriteRenderer>().sprite = backgroundImages[pageNum];
    }

    public void NextDesc()
    {
        pageNum = 1;
        if (languageType == 0)
            buttonText.text = "Before";
        else
            buttonText.text = "이전";
        LanguageChange(languageType);
        nextBeforeButton.onClick.AddListener(BeforeDesc);
    }

    public void BeforeDesc()
    {
        pageNum = 0;
        if (languageType == 0)
            buttonText.text = "Next";
        else
            buttonText.text = "다음";
        LanguageChange(languageType);
        nextBeforeButton.onClick.AddListener(NextDesc);
    }
}
