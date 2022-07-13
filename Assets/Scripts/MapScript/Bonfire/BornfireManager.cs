using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BornfireManager : MonoBehaviour
{
    public GameObject bonfireMenu, healBox, damageUpgradeBox, armorUpgradeBox, bonfireObject;
    public Text daggerText, leatherText, healAmountText, healText;
    public Sprite[] bonFireImages;
    GameObject gameManagerObject;
    int healAmount, languageType = 0;
    
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        leatherText.text = "3";
        LanguageChange(gameManagerObject.GetComponent<GameManager>().languageType);

        
        
        ItemManager itemManager = gameManagerObject.GetComponent<ItemManager>();
        int[] itemArray = itemManager.currentItem();
        if (itemArray[1] < 3)
        {
            leatherText.color = Color.red;
            armorUpgradeBox.SetActive(false);
        }

        StartCoroutine(OneTickAfter());
    }

    private void Update()
    {
        healAmount = gameManagerObject.GetComponent<StatusManager>().GetMaxhealth() / 5;
        healAmountText.text = healAmount + "";
    }

    public void Heal()
    {
        Debug.Log("Heal");
        bonfireMenu.SetActive(false);
        gameManagerObject.GetComponent<StatusManager>().ChangeHealth(healAmount);
        this.GetComponent<SceneByScene>().NextButtonON();
    }

    public void Upgrade(int num)
    {
        switch (num)
        {
            case 0:
                gameManagerObject.GetComponent<ItemManager>().itemChange(0, -3);
                gameManagerObject.GetComponent<StatusManager>().Upgrade(0);
                Debug.Log("Upgrade Damage");
                break;
            case 1:
                gameManagerObject.GetComponent<ItemManager>().itemChange(1, -3);
                gameManagerObject.GetComponent<StatusManager>().Upgrade(1);
                Debug.Log("Upgrade Armor");
                break;
        }
        bonfireMenu.SetActive(false);
    }
    IEnumerator OneTickAfter()
    {
        yield return new WaitForEndOfFrame();
        this.GetComponent<SceneByScene>().NextButtonON();
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        bonfireMenu.GetComponent<SpriteRenderer>().sprite = bonFireImages[languageType];
        if (languageType == 0) healText.text = "Heal";
        else healText.text = "회복";
    }
}
