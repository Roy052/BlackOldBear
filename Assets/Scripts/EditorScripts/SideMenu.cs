using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideMenu : MonoBehaviour
{
    public GameObject sideButton; // 사이드메뉴 닫기/열기 버튼
    public GameObject gridButton, webButton; // 그리드형, 거미줄형
    public TMP_Text loadText, bgmText, bpmText, beatText, NOWText, BOText, AOText, saveText;
    public TMP_InputField bpmInput, beatInput, NOWInput, BOInput, AOInput, fnInput;
    public TMP_Dropdown loadSelect, bgmSelect;
    public Button saveButton; // UI
    public Sprite sideButtonOpened, sideButtonClosed;
    public bool isOpened = false;
    SpriteRenderer sideButtonRenderer;
    List<TMP_Text> uiTexts;
    List<TMP_InputField> uiInputFields;
    List<TMP_Dropdown> uiDropdowns;
    List<Button> uiButtons;

    // Start is called before the first frame update
    void Start()
    {
        uiTexts = new List<TMP_Text> { loadText, bgmText, bpmText, beatText, NOWText, BOText, AOText, saveText };
        uiInputFields = new List<TMP_InputField> { bpmInput, beatInput, NOWInput, BOInput, AOInput, fnInput };
        uiDropdowns = new List<TMP_Dropdown> { loadSelect, bgmSelect };
        uiButtons = new List<Button> { saveButton };

        // bpmInput.onSubmit.AddListener(delegate { setBPM(); });
        
        uiSetActive(false);
        sideButtonRenderer = sideButton.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
