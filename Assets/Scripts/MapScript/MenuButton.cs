using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button start, quit;
    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        start.onClick.AddListener(gm.MenuToBattle);
        quit.onClick.AddListener(gm.GameExit);
    }

}
