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

    public void UITextUpdate(int maxHealth, int health, int money, int dagger, int leather)
    {
        if(maxHealth != -1 && health != -1)
            healthText.text = health + " / " + maxHealth;
        if (money != -1)
            moneyText.text = money + "";
        if (dagger != -1)
            daggerText.text = dagger + "";
        if (leather != -1)
            leatherText.text = leather + "";
    }
}
