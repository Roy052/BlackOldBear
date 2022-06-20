using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopMain;
    List<int> accessoryList;
    GameObject gameManagerObject;
    Accessory_Manager accessory_Manager;
    Accessory_Info accessory_Info;

    public Image[] images;
    public Text[] nameTexts, descTexts, priceTexts;
    public Button[] buyButtons;
    
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        accessory_Manager = gameManagerObject.GetComponent<Accessory_Manager>();
        accessory_Info = new Accessory_Info();

        if (accessory_Manager.shopAccessoryList != null 
            && accessory_Manager.shopAccessoryList.Count != 0 )
            accessoryList = accessory_Manager.shopAccessoryList;
        else
        {
            //생성
            accessoryList = new List<int>();

            //시작점
            int start = 0;
            int[] temp = accessory_Info.whereArray;
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
                if(random >= start + 2)
                {
                    accessoryList.Add(random);
                    break;
                }
            }

            for (int i = 1; i < 3; i++)
            {
                while (true)
                {
                    int random = Random.Range(start, accessoryList[i - 1]);
                    if (random >= start + 2 - i)
                    {
                        accessoryList.Add(random);
                        break;
                    }
                }
            }
            
            //완성
            accessory_Manager.shopAccessoryList = accessoryList;
        }

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i + "st : " + accessoryList[i] + ", ");
        }

        shopMain.SetActive(false);
    }
    public void ShopOpen()
    {
        shopMain.SetActive(true);
        for(int i = 0; i < 3; i++)
        {
            images[i].sprite = accessory_Manager.accessorySpriteArray[accessoryList[i]];
            nameTexts[i].text = accessory_Info.nameArray[accessoryList[i]];
            descTexts[i].text = accessory_Info.descriptionArray[accessoryList[i]];
            priceTexts[i].text = (100 * accessory_Info.rarityArray[accessoryList[i]] + Random.Range(0, 50)).ToString();
        }
    }

    public void ShopClose()
    {

    }
    public void Buy(int num)
    {
        accessory_Manager.AddAccessory(accessoryList[num]);
        images[num].gameObject.SetActive(false);
        nameTexts[num].gameObject.SetActive(false);
        descTexts[num].gameObject.SetActive(false);
        priceTexts[num].gameObject.SetActive(false);
        buyButtons[num].gameObject.SetActive(false);
    }
}
