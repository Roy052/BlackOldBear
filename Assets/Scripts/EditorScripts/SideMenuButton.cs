using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuButton : MonoBehaviour
{
    public GameObject sideMenuObj;
    SideMenu sideMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        sideMenuScript = sideMenuObj.GetComponent<SideMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // 열어야 할지 닫아야 할지는 사이드메뉴에게 맡김
        sideMenuScript.openOrClose();
    }
}
