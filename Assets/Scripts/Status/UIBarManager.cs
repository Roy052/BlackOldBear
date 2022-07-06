using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarManager : MonoBehaviour
{
    public GameObject UIBar;
    public Text healthText, moneyText, daggerText, leatherText;

    private void Start()
    {
        if(UIBar != null)
            DontDestroyOnLoad(UIBar);
    }

    public void UITextUpdate(int type_health_money_dagger_leather, int num, int num1_maxhealth)
    {
        switch (type_health_money_dagger_leather)
        {
            case 0:
                healthText.text = num + " / " + num1_maxhealth;
                break;
            case 1:
                moneyText.text = num + "";
                break;
            case 2:
                daggerText.text = num + "";
                break;
            case 3:
                leatherText.text = num + "";
                break;
        }
    }
    public void UIBarON()
    {
        UIBar.SetActive(true);
    }
    public void UIBarOFF()
    {
        UIBar.SetActive(false);
    }
}
