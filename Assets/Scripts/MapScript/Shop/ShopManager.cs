using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopMain;
    List<int> accessoryList;
    List<bool> accessoryAlreadyBuyList;
    GameObject gameManagerObject;
    Accessory_Manager accessory_Manager;
    Accessory_Info accessory_Info;
    ItemManager itemManager;
    int[] accessoryPrice;
    int[] itemPrice;

    public Image[] images;
    public Text[] nameTexts, descTexts, priceTexts, itemPriceTexts;
    public GameObject[] buyBoxes, itemBuyBoxes,itemImages;

    int languageType = 0;
    private void Start()
    {
        //초기화
        gameManagerObject = GameObject.Find("GameManager");
        accessory_Manager = gameManagerObject.GetComponent<Accessory_Manager>();
        accessory_Info = new Accessory_Info();
        itemManager = gameManagerObject.GetComponent<ItemManager>();

        this.gameObject.GetComponent<SceneByScene>().NextButtonON();

        //상점 품목 진열
        if (accessory_Manager.shopAccessoryList != null 
            && accessory_Manager.shopAccessoryList.Count != 0)
        {
            accessoryList = accessory_Manager.shopAccessoryList;
            accessoryAlreadyBuyList = accessory_Manager.shopAccessoryAlreadyBuyList;
        }
        else
        {
            //생성
            accessoryList = new List<int>();
            accessoryAlreadyBuyList = new List<bool>();

            //시작점
            int start = 0;
            int[] temp = accessory_Info.whereArray;
            int accessoryOwnCount = accessory_Manager.accessaryOwnCount;
            for (int i = 0; i < temp.Length; i++)
            {
                Debug.Log(temp[i]);
            }

            for (int i = 0; i < temp.Length; i++)
                if (temp[i] == 0) //Shop 악세서리라면
                {
                    start = i;
                    break;
                }

            while (true)
            {
                int random = Random.Range(start, temp.Length);
                if(accessory_Manager.IsAccessoryOwn(random) == false)
                {
                    if (random >= start + 2 + accessoryOwnCount)
                    {
                        accessoryList.Add(random);
                        break;
                    }
                }  
            }

            for (int i = 1; i < 3; i++)
            {
                while (true)
                {
                    int random = Random.Range(start, accessoryList[i - 1]);
                    if (accessory_Manager.IsAccessoryOwn(random) == false)
                    {
                        if (random >= start + 2 - i + accessoryOwnCount)
                        {
                            accessoryList.Add(random);
                            break;
                        }
                    }
                }
            }
            
            //완성
            accessory_Manager.shopAccessoryList = accessoryList;
            for(int i = 0; i < 3; i++)
                accessoryAlreadyBuyList.Add(false);
            accessory_Manager.shopAccessoryAlreadyBuyList = accessoryAlreadyBuyList;
        }

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i + "st : " + accessoryList[i] + ", ");
        }

        shopMain.SetActive(false);

        //초기화 2
        accessoryPrice = new int[3];
        itemPrice = new int[2];
        for (int i = 0; i < 3; i++)
            accessoryPrice[i] = 100 * accessory_Info.rarityArray[accessoryList[i]] + Random.Range(0, 50);
        for (int i = 0; i < 2; i++)
            itemPrice[i] = 20 + Random.Range(0, 10);
    }
    public void ShopOpen()
    {
        for(int i = 0; i < 3; i++)
        {
            if(accessoryAlreadyBuyList[i] == false)
            {
                images[i].sprite = accessory_Manager.accessorySpriteArray[accessoryList[i]];
                nameTexts[i].text = accessory_Info.nameArray[gameManagerObject.GetComponent<GameManager>().languageType, accessoryList[i]];
                descTexts[i].text = accessory_Info.descriptionArray[gameManagerObject.GetComponent<GameManager>().languageType, accessoryList[i]];
                priceTexts[i].text = accessoryPrice[i].ToString();
            }
            else
            {
                images[i].gameObject.SetActive(false);
                nameTexts[i].gameObject.SetActive(false);
                descTexts[i].gameObject.SetActive(false);
                priceTexts[i].gameObject.SetActive(false);
                buyBoxes[i].SetActive(false);
            }

            if (itemManager.currentMoney() < accessoryPrice[i])
            {
                priceTexts[i].color = Color.red;
                buyBoxes[i].SetActive(false);
            } 
        }

        for(int i = 0; i < 2; i++)
        {
            itemPriceTexts[i].text = itemPrice[i].ToString();
            if (itemManager.currentMoney() < itemPrice[i])
            {
                itemPriceTexts[i].color = Color.red;
                itemBuyBoxes[i].SetActive(false);
            }
        }

        shopMain.SetActive(true);
    }
    public void ShopUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            if (itemManager.currentMoney() < accessoryPrice[i])
            {
                priceTexts[i].color = Color.red;
                buyBoxes[i].SetActive(false);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (itemManager.currentMoney() < itemPrice[i])
            {
                itemPriceTexts[i].color = Color.red;
                itemBuyBoxes[i].SetActive(false);
            }
        }

    }
    public void ShopClose()
    {

    }
    public void Buy(int type,int num)
    {
        if(type == 0)
        {
            itemManager.moneyChange(-accessoryPrice[num]);
            accessory_Manager.AddAccessory(accessoryList[num]);
            images[num].gameObject.SetActive(false);
            nameTexts[num].gameObject.SetActive(false);
            descTexts[num].gameObject.SetActive(false);
            priceTexts[num].gameObject.SetActive(false);
            buyBoxes[num].gameObject.SetActive(false);

            accessoryAlreadyBuyList[num] = true;
        }
        else if(type == 1)
        {
            itemManager.moneyChange(-itemPrice[num]);
            itemManager.itemChange(num, 1);
        }
        else
        {

        }
        
        ShopUpdate();
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        for (int i = 0; i < 3; i++)
        {
            if (accessoryAlreadyBuyList[i] == false)
            {
                nameTexts[i].text = accessory_Info.nameArray[languageType, accessoryList[i]];
                descTexts[i].text = accessory_Info.descriptionArray[languageType, accessoryList[i]];
                priceTexts[i].text = accessoryPrice[i].ToString();
            }
            else
            {
                images[i].gameObject.SetActive(false);
                nameTexts[i].gameObject.SetActive(false);
                descTexts[i].gameObject.SetActive(false);
                priceTexts[i].gameObject.SetActive(false);
                buyBoxes[i].SetActive(false);
            }
        }
    }
}
