using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SideMenu : MonoBehaviour
{
    public List<AudioClip> musics = new(){ null }; // AudioClip 리스트
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
    public List<string> bgmTexts, loadTexts;

    [HideInInspector]
    public List<int> FPSs;


    // Start is called before the first frame update
    void Start()
    {
        uiTexts = new List<TMP_Text> { loadText, bgmText, bpmText, beatText, NOWText, BOText, AOText, speedText, saveText };
        uiInputFields = new List<TMP_InputField> { bpmInput, beatInput, NOWInput, BOInput, AOInput, speedInput, fnInput };
        uiDropdowns = new List<TMP_Dropdown> { loadSelect, bgmSelect };
        uiButtons = new List<Button> { saveButton };

        lineManageScript = lineManageObj.GetComponent<LineManageScript>();
        musicManageScript = musicManageObj.GetComponent<MusicManage>();

        bgmTexts = new()
        {
            "Select Music",
            "Electronic"
        };

        loadTexts = new()
        {
            "Select File"
        };

        FPSs = new()
        {
            120,
            140 // 1. Electronic
        };

        SetBGMList();
        SetLoadLists();

        bpmInput.text = "120";
        beatInput.text = "4";
        NOWInput.text = "10";
        BOInput.text = "0";
        AOInput.text = "20";
        speedInput.text = "5";
        fnInput.text = "NewPattern";

        bpmInput.onEndEdit.AddListener(delegate { lineManageScript.setBPM(float.Parse(bpmInput.text)); });
        beatInput.onSubmit.AddListener(delegate { lineManageScript.setBeat(int.Parse(beatInput.text)); });
        NOWInput.onSubmit.AddListener(delegate { lineManageScript.setSegments(int.Parse(NOWInput.text)); });
        BOInput.onSubmit.AddListener(delegate { lineManageScript.setBaseOffset(float.Parse(BOInput.text)); });
        AOInput.onSubmit.AddListener(delegate { lineManageScript.setBaseAngle(float.Parse(AOInput.text)); });

        speedInput.onSubmit.AddListener(delegate { lineManageScript.setSpeed(float.Parse(speedInput.text)); });
        bgmSelect.onValueChanged.AddListener(delegate { setMusic(); });
        loadSelect.onValueChanged.AddListener(delegate { loadData(loadTexts[loadSelect.value]); });

        saveButton.onClick.AddListener(delegate { saveData(); });

        uiSetActive(false);
        sideButtonRenderer = sideButton.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBGMList()
    {
        // BGM 데이터
        bgmSelect.options.Clear();
        for (int i = 0; i < bgmTexts.Count; i++)
        {
            TMP_Dropdown.OptionData option = new();
            option.text = bgmTexts[i];
            bgmSelect.options.Add(option);
        }
    }

    void SetLoadLists()
    {
        // Load 데이터
        loadSelect.options.Clear();
        loadTexts = new()
        {
            "Select File"
        };

        TMP_Dropdown.OptionData Firstoption = new();
        Firstoption.text = loadTexts[0];
        loadSelect.options.Add(Firstoption);

        string FolderName = "./Assets/Patterns/";
        DirectoryInfo di = new DirectoryInfo(FolderName);
        foreach (FileInfo File in di.GetFiles())
        {
            if (File.Extension.ToLower().CompareTo(".json") == 0)
            {
                string FileNameOnly = File.Name.Substring(0, File.Name.Length - 5);
                loadTexts.Add(FileNameOnly);

                TMP_Dropdown.OptionData option = new();
                option.text = FileNameOnly;
                loadSelect.options.Add(option);
            }
        }

        loadSelect.value = 0;
    }

    void setMusic()
    {
        musicManageScript.setMusic(musics[bgmSelect.value]);
        bpmInput.text = FPSs[bgmSelect.value].ToString();
    }

    void saveData()
    {
        string filename = fnInput.text;
        string bgm = null;
        if (musicManageScript.mSource.clip != null)
        {
            bgm = musicManageScript.mSource.clip.name;
        }
        int diff = 1; // 난이도 설정은 나중에 추가

        if (fnInput.text != "" && bgm != null)
            lineManageScript.saveData(fnInput.text, bgm, diff);

        SetLoadLists();
    }

    void loadData(string filename)
    {
        // 그냥 드롭다운 번호가 0보다 큰지 체크해도 됐는데
        // 이 if문은 드롭다운에서 0번을 거르는 동시에
        // 드롭다운에서 호출된 게 아닌 경우 파일명만으로 체크하기 위함
        if (File.Exists("./Assets/Patterns/" + filename + ".json"))
        {
            PatternData pData = lineManageScript.loadData(filename);

            int bgmIndex = bgmTexts.FindIndex(x => x == pData.BGM);
            bgmSelect.value = bgmIndex;
            bpmInput.text = pData.BPM.ToString();
            beatInput.text = pData.beat.ToString();
            NOWInput.text = pData.segments.ToString();
            BOInput.text = pData.baseOffset.ToString();
            AOInput.text = pData.baseAngle.ToString();
            speedInput.text = pData.speed.ToString();
            fnInput.text = filename;
        }
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
