using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorTutorial : MonoBehaviour
{
    public Sprite[] images;
    public Text clickText;

    int languageType = 0;
    private void Start()
    {
        GameObject temp = GameObject.Find("GameManager");
        if (temp != null)
        {
            languageType = temp.GetComponent<GameManager>().languageType;
        }
        else languageType = 1;

        LanguageChange(languageType);
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        this.GetComponent<SpriteRenderer>().sprite = images[languageType];
        if (languageType == 0) clickText.text = "... Click to close";
        else clickText.text = "... 클릭해서 창 닫기";
    }

    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
    }
}
