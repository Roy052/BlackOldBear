using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory_Manager : MonoBehaviour
{
    public GameObject accessoryPrefab;
    public Sprite[] accessorySpriteArray;
    public Accessory_Info accessoryInfo;
    public List<int> shopAccessoryList;

    float startX = -8.2f, startY = 4.4f, gap = 1.2f;
    List<GameObject> accessoryList = new List<GameObject>();

    private void Start()
    {
        accessoryInfo = new Accessory_Info();
        shopAccessoryList = new List<int>();
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

        accessoryList.Add(clone);
        DontDestroyOnLoad(clone);

        clone.SetActive(true);
    }
}
