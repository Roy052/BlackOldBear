using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory_Manager : MonoBehaviour
{
    public GameObject accessoryPrefab;
    public Sprite[] accessorySpriteArray;
    public Accessory_Info accessoryInfo;
    public List<int> shopAccessoryList;
    public List<bool> shopAccessoryAlreadyBuyList;
    public int accessaryOwnCount = 0;

    float startX = -8.9f, startY = 4.1f, gap = 1.2f;
    List<GameObject> accessoryList = new List<GameObject>();
    bool[] accessoryOwnList;
    GameManager gameManager;
    private void Start()
    {
        accessoryInfo = new Accessory_Info();
        shopAccessoryList = new List<int>();
        accessoryOwnList = new bool[accessoryInfo.nameArray.Length];
        gameManager = this.GetComponent<GameManager>();
    }

    public bool IsAccessoryOwn(int num)
    {
        return accessoryOwnList[num];
    }

    public void AddAccessory(int num)
    {
        GameObject clone = Instantiate(accessoryPrefab, 
            new Vector3(startX + gap * accessoryList.Count, startY, 0), Quaternion.identity);
        clone.SetActive(false);

        clone.GetComponent<SpriteRenderer>().sprite = accessorySpriteArray[num];

        Accessory cloneAccessory = clone.GetComponent<Accessory>();
        accessoryInfo = new Accessory_Info();

        cloneAccessory.accessoryNum = num;
        cloneAccessory.rarity = accessoryInfo.rarityArray[num];
        cloneAccessory.type = accessoryInfo.typeArray[num];
        cloneAccessory.where = accessoryInfo.whereArray[num];
        cloneAccessory.thumb = accessorySpriteArray[num];
        cloneAccessory.accessoryName = accessoryInfo.nameArray[gameManager.languageType ,num];
        cloneAccessory.description = accessoryInfo.descriptionArray[gameManager.languageType, num] ;
        cloneAccessory.additionalText = accessoryInfo.additionalTextArray[gameManager.languageType, num];

        accessoryOwnList[num] = true;
        accessaryOwnCount++;

        accessoryList.Add(clone);
        DontDestroyOnLoad(clone);
        
        clone.SetActive(true);
    }

    public void AccessoryReset()
    {
        for(int i = accessoryList.Count - 1; i >= 0; i--)
        {
            Destroy(accessoryList[i]);
            accessoryList.Remove(accessoryList[i]);
        }

        shopAccessoryList = new List<int>();
        accessoryOwnList = new bool[accessoryInfo.nameArray.Length];
        shopAccessoryAlreadyBuyList = new List<bool>();
    }
}
