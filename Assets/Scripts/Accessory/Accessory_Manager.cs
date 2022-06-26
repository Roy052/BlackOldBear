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

    float startX = -8.2f, startY = 4.4f, gap = 1.2f;
    List<GameObject> accessoryList = new List<GameObject>();
    bool[] accessoryOwnList;
    private void Start()
    {
        accessoryInfo = new Accessory_Info();
        shopAccessoryList = new List<int>();
        accessoryOwnList = new bool[accessoryInfo.nameArray.Length];
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
        cloneAccessory.accessoryName = accessoryInfo.nameArray[num];
        cloneAccessory.description = accessoryInfo.descriptionArray[num];
        cloneAccessory.additionalText = accessoryInfo.additionalTextArray[num];

        accessoryOwnList[num] = true;
        accessaryOwnCount++;

        accessoryList.Add(clone);
        DontDestroyOnLoad(clone);
        
        clone.SetActive(true);
    }
}
