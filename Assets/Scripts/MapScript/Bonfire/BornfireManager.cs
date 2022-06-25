using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BornfireManager : MonoBehaviour
{
    public GameObject bornfireMenu, healBox, damageUpgradeBox, armorUpgradeBox;
    public Text daggerText, leatherText;
    GameObject gameManagerObject;
    SceneByScene sbs;
    
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        sbs = this.gameObject.GetComponent<SceneByScene>();
        daggerText.text = "3";
        leatherText.text = "3";
        ItemManager itemManager = gameManagerObject.GetComponent<ItemManager>();
        int[] itemArray = itemManager.currentItem();
        if (itemArray[0] < 3)
        {
            daggerText.color = Color.red;
            damageUpgradeBox.SetActive(false);
        }
        if (itemArray[1] < 3)
        {
            leatherText.color = Color.red;
            armorUpgradeBox.SetActive(false);
        }
    }

    public void Heal()
    {
        Debug.Log("Heal");
        bornfireMenu.SetActive(false);
        sbs.NextButtonON();
    }

    public void Upgrade(int num)
    {
        switch (num)
        {
            case 0:
                gameManagerObject.GetComponent<ItemManager>().itemChange(0, -3);
                Debug.Log("Upgrade Damage");
                break;
            case 1:
                gameManagerObject.GetComponent<ItemManager>().itemChange(1, -3);
                Debug.Log("Upgrade Armor");
                break;
        }
        bornfireMenu.SetActive(false);
        sbs.NextButtonON();
    }
}
