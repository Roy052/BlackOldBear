using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public GameObject sideButton; // 사이드메뉴 닫기/열기 버튼
    public GameObject gridButton, webButton; // 그리드형, 거미줄형
    public Sprite sideButtonOpened, sideButtonClosed;
    public bool isOpened = false;
    SpriteRenderer sideButtonRenderer; 

    // Start is called before the first frame update
    void Start()
    {
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

    void openMenu()
    {
        transform.position = new Vector3(6.7f, 0f, -3f);
        sideButton.transform.position = new Vector3(4.8f, 0f, -3.1f);
        gridButton.transform.position = new Vector3(7f, -4.3f, -3.1f);
        webButton.transform.position = new Vector3(8.2f, -4.3f, -3.1f);
        sideButtonRenderer.sprite = sideButtonOpened;
        isOpened = true;
    }

    void closeMenu()
    {
        transform.position = new Vector3(10.5f, 0, -3);
        sideButton.transform.position = new Vector3(8.6f, 0f, -3.1f);
        gridButton.transform.position = new Vector3(10.8f, -4.3f, -3.1f);
        webButton.transform.position = new Vector3(12f, -4.3f, -3.1f);
        sideButtonRenderer.sprite = sideButtonClosed;
        isOpened = false;
    }
}
