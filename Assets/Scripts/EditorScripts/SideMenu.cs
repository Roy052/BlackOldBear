using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideMenu : MonoBehaviour
{
    public List<AudioClip> musics = new(){ null };
    public GameObject lineManageObj, musicManageObj;
    public GameObject sideButton; // 사이드메뉴 닫기/열기 버튼
    public GameObject gridButton, webButton; // 그리드형, 거미줄형
    public TMP_Text loadText, bgmText, bpmText, beatText, NOWText, BOText, AOText, speedText, saveText;
    public TMP_InputField bpmInput, beatInput, NOWInput, BOInput, AOInput, speedInput, fnInput;
    public TMP_Dropdown loadSelect, bgmSelect;
    public Button saveButton; // UI
    public Sprite sideButtonOpened, sideButtonClosed;
    
    [HideInInspector]
    public bool isOpened = false;

    [HideInInspector]
    public int selectedMusic = 0;

    LineManageScript lineManageScript;
    MusicManage musicManageScript;
    SpriteRenderer sideButtonRenderer;

    List<TMP_Text> uiTexts;
    List<TMP_InputField> uiInputFields;
    List<TMP_Dropdown> uiDropdowns;
    List<Button> uiButtons;

    [HideInInspector]
    public List<string> optionTexts = new() 
    { 
        "Select Music",
        "1. Electronic" 
    };

    [HideInInspector]
    public List<int> FPSs = new()
    {
        120,
        140 // 1. Electronic
    };

    // Start is called before the first frame update
    void Start()
    {
        uiTexts = new List<TMP_Text> { loadText, bgmText, bpmText, beatText, NOWText, BOText, AOText, speedText, saveText };
        uiInputFields = new List<TMP_InputField> { bpmInput, beatInput, NOWInput, BOInput, AOInput, speedInput, fnInput };
        uiDropdowns = new List<TMP_Dropdown> { loadSelect, bgmSelect };
        uiButtons = new List<Button> { saveButton };

        lineManageScript = lineManageObj.GetComponent<LineManageScript>();
        musicManageScript = musicManageObj.GetComponent<MusicManage>();

        SetDropdownOptions();

        bpmInput.text = "120";
        beatInput.text = "4";
        NOWInput.text = "10";
        BOInput.text = "0";
        AOInput.text = "20";
        speedInput.text = "5";
        fnInput.text = "NewPattern";

        bpmInput.onSubmit.AddListener(delegate { lineManageScript.setBPM(float.Parse(bpmInput.text)); });
        beatInput.onSubmit.AddListener(delegate { lineManageScript.setBeat(int.Parse(beatInput.text)); });
        NOWInput.onSubmit.AddListener(delegate { lineManageScript.setSegments(int.Parse(NOWInput.text)); });
        speedInput.onSubmit.AddListener(delegate { lineManageScript.setSpeed(float.Parse(speedInput.text)); });
        bgmSelect.onValueChanged.AddListener(delegate { setMusic(); });

        uiSetActive(false);
        sideButtonRenderer = sideButton.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDropdownOptions()// Dropdown 목록 생성
    {
        bgmSelect.options.Clear();
        for (int i = 0; i < optionTexts.Count; i++)
        {
            TMP_Dropdown.OptionData option = new();
            option.text = optionTexts[i];
            bgmSelect.options.Add(option);
        }
    }

    void setMusic()
    {
        musicManageScript.setMusic(musics[bgmSelect.value]);
        bpmInput.text = FPSs[bgmSelect.value].ToString();
    }

    public void openOrClose()
    {
        if (isOpened)
        {
            closeMenu();
        }
        else
        {
            openMenu();
        }
    }

    void uiSetActive(bool isActive)
    {
        foreach (TMP_Text ui in uiTexts)
        {
            ui.gameObject.SetActive(isActive);
        }
        foreach (TMP_InputField ui in uiInputFields)
        {
            ui.gameObject.SetActive(isActive);
        }
        foreach (TMP_Dropdown ui in uiDropdowns)
        {
            ui.gameObject.SetActive(isActive);
        }
        foreach (Button ui in uiButtons)
        {
            ui.gameObject.SetActive(isActive);
        }
    }

    void openMenu()
    {
        transform.position = new Vector3(6.7f, 0f, -3f);
        sideButton.transform.position = new Vector3(4.8f, 0f, -3.1f);
        gridButton.transform.position = new Vector3(7f, -4.3f, -3.1f);
        webButton.transform.position = new Vector3(8.2f, -4.3f, -3.1f);
        sideButtonRenderer.sprite = sideButtonOpened;
        isOpened = true;
        uiSetActive(true);
    }

    void closeMenu()
    {
        transform.position = new Vector3(10.5f, 0, -3);
        sideButton.transform.position = new Vector3(8.6f, 0f, -3.1f);
        gridButton.transform.position = new Vector3(10.8f, -4.3f, -3.1f);
        webButton.transform.position = new Vector3(12f, -4.3f, -3.1f);
        sideButtonRenderer.sprite = sideButtonClosed;
        isOpened = false;
        uiSetActive(false);
    }
}
