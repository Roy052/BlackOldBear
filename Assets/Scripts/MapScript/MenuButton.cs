using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button start, setting, pattern, quit;
    public Text headText, startText, settingText, patternText, quitText;
    public Text headText_shadow, startText_shadow, settingText_shadow, patternText_shadow, quitText_shadow;
    public GameObject Banner;
    GameManager gm;
    float time = 0;
    AudioSource audioSource;
    
    void Start()
    {
        Banner.SetActive(false);
        start.gameObject.SetActive(false);
        setting.gameObject.SetActive(false);
        pattern.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);

        headText.gameObject.SetActive(false);
        startText.gameObject.SetActive(false);
        settingText.gameObject.SetActive(false);
        patternText.gameObject.SetActive(false);
        quitText.gameObject.SetActive(false);

        headText_shadow.gameObject.SetActive(false);
        startText_shadow.gameObject.SetActive(false);
        settingText_shadow.gameObject.SetActive(false);
        patternText_shadow.gameObject.SetActive(false);
        quitText_shadow.gameObject.SetActive(false);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        start.onClick.AddListener(gm.MenuToBattle);
        setting.onClick.AddListener(gm.PauseMenuON);
        pattern.onClick.AddListener(gm.ToPattern);
        quit.onClick.AddListener(gm.GameExit);

        LanguageChange(gm.languageType);

        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(UION());
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        
        if(time >= 3)
        {
            audioSource.Play();
            time = 0;
        }
    }
    IEnumerator UION()
    {
        
        yield return new WaitForSeconds(2);

        Banner.SetActive(true);
        headText.gameObject.SetActive(true);
        headText_shadow.gameObject.SetActive(true);
        StartCoroutine(FadeManager.FadeOut(Banner.GetComponent<SpriteRenderer>(), 1));
        StartCoroutine(TextFadeOut(headText));
        StartCoroutine(TextFadeOut(headText_shadow));

        yield return new WaitForSeconds(1);

        start.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
        startText_shadow.gameObject.SetActive(true);
        StartCoroutine(ButtonFadeOut(start));
        StartCoroutine(TextFadeOut(startText));
        StartCoroutine(TextFadeOut(startText_shadow));

        setting.gameObject.SetActive(true);
        settingText.gameObject.SetActive(true);
        settingText_shadow.gameObject.SetActive(true);
        StartCoroutine(ButtonFadeOut(setting));
        StartCoroutine(TextFadeOut(settingText));
        StartCoroutine(TextFadeOut(settingText_shadow));

        pattern.gameObject.SetActive(true);
        patternText.gameObject.SetActive(true);
        patternText_shadow.gameObject.SetActive(true);
        StartCoroutine(ButtonFadeOut(pattern));
        StartCoroutine(TextFadeOut(patternText));
        StartCoroutine(TextFadeOut(patternText_shadow));

        quit.gameObject.SetActive(true);
        quitText.gameObject.SetActive(true);
        quitText_shadow.gameObject.SetActive(true);
        StartCoroutine(ButtonFadeOut(quit));
        StartCoroutine(TextFadeOut(quitText));
        StartCoroutine(TextFadeOut(quitText_shadow));
    }

    IEnumerator TextFadeOut(Text text)
    {
        Color temp = text.color;
        temp.a = 0;

        text.color = temp;
        while (temp.a < 1)
        {
            temp.a += Time.fixedDeltaTime;
            text.color = temp;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ButtonFadeOut(Button button)
    {
        Color temp = button.image.color;
        temp.a = 0;

        button.image.color = temp;
        while (temp.a < 1)
        {
            temp.a += Time.fixedDeltaTime;
            button.image.color = temp;
            yield return new WaitForFixedUpdate();
        }
    }

    public void LanguageChange(int type)
    {
        if(type == 0)
        {
            startText.text = "Start";
            startText_shadow.text = "Start";
            settingText.text = "Setting";
            settingText_shadow.text = "Setting";
            patternText.text = "Edit Note";
            patternText_shadow.text = "Edit Note";
            quitText.text = "Quit";
            quitText_shadow.text = "Quit";
        }
        else
        {
            startText.text = "시작하기";
            startText_shadow.text = "시작하기";
            settingText.text = "설정";
            settingText_shadow.text = "설정";
            patternText.text = "패턴 짜기";
            patternText_shadow.text = "패턴 짜기";
            quitText.text = "종료하기";
            quitText_shadow.text = "종료하기";
        }
    }
}
