using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RewardManager : MonoBehaviour
{
    public GameObject rewardBackground;
    public GameObject[] rewardboxes;
    public Image[] icons;
    public Text[] texts;
    public Sprite money;
    public Sprite[] itemSprite;
    public float ratio;

    int mapNum;
    int[] rewardType; //0 : 빈 것, 1 : 돈, 2 : 재료, 3 : 악세서리
    int[] rewardValue;
    GameObject gameManagerObject;
    Accessory_Manager accessory_Manager;
    Accessory_Info accessory_Info;
    EventManager eventManager;

    private void Start()
    {
        rewardType = new int[3];
        rewardValue = new int[3];

        rewardBackground.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            rewardType[i] = 0;
            rewardValue[i] = 0;
            rewardboxes[i].SetActive(false);
            icons[i].gameObject.SetActive(false);
            texts[i].gameObject.SetActive(false);
        }

        gameManagerObject = GameObject.Find("GameManager");
        accessory_Manager = gameManagerObject.GetComponent<Accessory_Manager>();
        accessory_Info = new Accessory_Info();

        mapNum = SceneManager.GetActiveScene().buildIndex - 1;
        
    }

    public int RandomAccessory(int rarity)
    {
        int value;
        int[] rarityArray = accessory_Info.rarityArray;
        int[] whereArray = accessory_Info.whereArray;
        int start = -1, end = -1, max = whereArray.Length;

        for (int i = 0; i < max; i++)
        {
            if (start == -1 && whereArray[i] == 1 && rarityArray[i] == rarity)
            {
                start = i;
            }
            if (start != -1 && (whereArray[i] > 1 || rarityArray[i] > rarity))
            {
                end = i;
                break;
            }
            if (i == max - 1) end = i;
        }
        value = Random.Range(start, end);

        while(true)
        {
            if (accessory_Manager.IsAccessoryOwn(value) == false) break;
            value = Random.Range(start, end);
        }

        return value;
    }

    public void RewardON()
    {
        switch (mapNum)
        {
            case 5: //Event
                eventManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EventManager>();
                switch (eventManager.GetEventNum())
                {
                    case 0:
                        rewardType[0] = 1;
                        rewardValue[0] = 100;
                        break;
                    case 1:
                        rewardType[0] = 2;
                        rewardValue[0] = 1;
                        rewardType[1] = 2;
                        rewardValue[1] = 1;
                        break;
                    case 2:
                        break;
                }

                break;
            case 6: //Chest
                rewardType[0] = 3;
                rewardValue[0] = RandomAccessory(1);
                break;
            case 7: //Enemy
                rewardType[0] = 1;
                rewardValue[0] = Random.Range(17, 24);
                rewardType[1] = 2;
                rewardValue[1] = 1;
                break;
            case 8: //MidBoss
                rewardType[0] = 1;
                rewardValue[0] = 100 + Random.Range(-5, 5);
                rewardType[1] = 3;
                rewardValue[1] = RandomAccessory(1);
                break;
            case 9: //Boss
                rewardType[0] = 1;
                rewardValue[0] = 150 + Random.Range(-10, 10); ;
                rewardType[1] = 2;
                rewardValue[1] = 1;
                rewardType[2] = 3;
                rewardValue[2] = RandomAccessory(3);
                break;
            default:
                rewardType[0] = 1;
                rewardValue[0] = Random.Range(17, 24);
                if (ratio >= 0.9)
                {
                    rewardValue[0] += Random.Range(5, 10);
                }
                else if (ratio >= 0.8)
                {
                    rewardValue[0] += Random.Range(2, 7);
                }
                rewardType[1] = 2;
                rewardValue[1] = 1;
                break;
        }
        rewardBackground.SetActive(true);

        for(int i = 0; i < 3; i++)
        {
            switch (rewardType[i])
            {
                case 0:
                    break;
                case 1: //Money
                    icons[i].sprite = money;
                    texts[i].text = rewardValue[i].ToString();
                    rewardboxes[i].gameObject.SetActive(true);
                    icons[i].gameObject.SetActive(true);
                    texts[i].gameObject.SetActive(true);
                    break;
                case 2: //Item
                    icons[i].sprite = itemSprite[rewardValue[i]];
                    texts[i].text = rewardValue[i] == 0 ? "Dagger" : "Leather";
                    rewardboxes[i].gameObject.SetActive(true);
                    icons[i].gameObject.SetActive(true);
                    texts[i].gameObject.SetActive(true);
                    break;
                case 3: //Accessory
                    icons[i].sprite = accessory_Manager.accessorySpriteArray[rewardValue[i]];
                    texts[i].text = accessory_Info.nameArray[rewardValue[i]];

                    rewardboxes[i].gameObject.SetActive(true);
                    icons[i].gameObject.SetActive(true);
                    texts[i].gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void RewardOFF()
    {
        for (int i = 0; i < 3; i++)
        {
            rewardboxes[i].SetActive(false);
            icons[i].gameObject.SetActive(false);
            texts[i].gameObject.SetActive(false);
        }
        rewardBackground.SetActive(false);
    }

    public void GainReward(int num)
    {
        Debug.Log("Reward : " + num);
        switch (rewardType[num])
        {
            case 0:
                break;
            case 1:
                gameManagerObject.GetComponent<ItemManager>().moneyChange(rewardValue[num]);
                break;
            case 2:
                gameManagerObject.GetComponent<ItemManager>().itemChange(rewardValue[num], 1);
                break;
            case 3:
                accessory_Manager.AddAccessory(rewardValue[num]);
                break;
        }
        rewardboxes[num].SetActive(false);
        icons[num].gameObject.SetActive(false);
        texts[num].gameObject.SetActive(false);
    }
}
