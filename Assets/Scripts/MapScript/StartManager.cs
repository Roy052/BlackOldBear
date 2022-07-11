using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject background;
    public Sprite[] backgroundImages;
    GameManager gm;
    int languageType = 0;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        LanguageChange(gm.languageType);
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        background.GetComponent<SpriteRenderer>().sprite = backgroundImages[languageType];
    }
}
