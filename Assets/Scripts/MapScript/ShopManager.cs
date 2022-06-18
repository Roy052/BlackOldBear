using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    GameObject shopMain;
    List<int> accessoryList;
    GameObject gameManager;
    Accessory_Manager accessory_Manager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        accessory_Manager = gameManager.GetComponent<Accessory_Manager>();
        if (accessory_Manager.shopAccessoryList.Count != 0)
            accessoryList = accessory_Manager.shopAccessoryList;
        else
        {
            //생성
            accessoryList = new List<int>();

            //시작점
            int start = 0;
            if (accessory_Manager.accessoryInfo == null) Debug.Log("A");
            int[] temp = accessory_Manager.accessoryInfo.rarityArray;
            for (int i = 0; i < temp.Length; i++)
                if (temp[i] == 0) //Shop 악세서리라면
                    start = i;

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
                int random = Random.Range(start, accessoryList[i-1]);
                if (random >= start + 2 - i)
                {
                    accessoryList.Add(random);
                    break;
                }
            }

            //완성
            accessory_Manager.shopAccessoryList = accessoryList;
        }
          
    }
    public void ShopOpen()
    {
        
    }

    public void ShopClose()
    {

    }
}
