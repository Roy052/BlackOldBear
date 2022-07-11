using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    List<string> eventSceneList;
    
    Event_Info event_Info = new Event_Info();
    int eventNum = 0;
    GameObject gameManagerObject;

    public int currentEvent;
    public Button yesButton, noButton;
    public Text descriptionText, yesText, noText;
    
    public SpriteRenderer backroundImage;
    public Sprite[] backgroundImageSprites;
    public GameObject hands, eventDescriptionBackground;

    int languageType = 0;
    private void Start()
    {
        eventSceneList = new List<string>();
        eventSceneList.Add("Mini-AimBooster");
        eventSceneList.Add("RunningBear");
        eventSceneList.Add("RememberCards");
        gameManagerObject = GameObject.Find("GameManager");
        languageType = gameManagerObject.GetComponent<GameManager>().languageType;

        eventNum = Random.Range(0, event_Info.eventConditionType.Length);

        backroundImage.sprite = backgroundImageSprites[eventNum];
        descriptionText.text = event_Info.eventDescription[languageType, eventNum];
        
        if(event_Info.eventConditionType[eventNum] != 0)
        {
            switch (event_Info.eventConditionType[eventNum])
            {
                case 1:
                    if (event_Info.eventConditionValue[eventNum] > gameManagerObject.GetComponent<ItemManager>().currentMoney())
                    {
                        yesText.color = Color.red;
                        yesButton.interactable = false;
                    }
                    break;
                case 2:
                    if (event_Info.eventConditionValue[eventNum] 
                        > gameManagerObject.GetComponent<ItemManager>().currentItem()[0])
                    {
                        yesText.color = Color.red;
                        yesButton.interactable = false;
                    }
                    break;
                case 3:
                    if (event_Info.eventConditionValue[eventNum]
                        > gameManagerObject.GetComponent<ItemManager>().currentItem()[1])
                    {
                        yesText.color = Color.red;
                        yesButton.interactable = false;
                    }
                    break;
                case 4:
                    if (event_Info.eventConditionValue[eventNum]
                        > gameManagerObject.GetComponent<StatusManager>().GetHealth())
                    {
                        yesText.color = Color.red;
                        yesButton.interactable = false;
                    }
                    break;
            }
            yesText.text = event_Info.eventConditionValue[eventNum] + (languageType == 0 ? "Use, " : " 소모, ") + event_Info.eventChoice[languageType, eventNum, 0];
        }
        else
        {
            yesText.text = event_Info.eventChoice[languageType, eventNum, 0];
        }
        noText.text = event_Info.eventChoice[languageType, eventNum, 1];
        
    }

    public void EventStart()
    {
        yesButton.interactable = false;
        noButton.interactable = false;

        //Cost
        switch (event_Info.eventConditionType[eventNum])
        {
            case 1:
                gameManagerObject.GetComponent<ItemManager>().moneyChange(-event_Info.eventConditionValue[eventNum]);
                break;
            case 2:
                gameManagerObject.GetComponent<ItemManager>().itemChange(0, -event_Info.eventConditionValue[eventNum]);
                break;
            case 3:
                gameManagerObject.GetComponent<ItemManager>().itemChange(1, -event_Info.eventConditionValue[eventNum]);
                break;
            case 4:
                gameManagerObject.GetComponent<StatusManager>().ChangeHealth(-event_Info.eventConditionValue[eventNum]);
                break;
        }

        if (eventNum < eventSceneList.Count)
        {
            SceneManager.LoadSceneAsync(eventSceneList[eventNum], LoadSceneMode.Additive);
            hands.SetActive(false);
            backroundImage.sprite = null;
            eventDescriptionBackground.SetActive(false);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
        }
            
        else
        {
            //Reward
            switch (eventNum)
            {
                case 3:
                    gameManagerObject.GetComponent<ItemManager>().itemChange(0, 3);
                    break;
                case 4:
                    gameManagerObject.GetComponent<ItemManager>().moneyChange(50);
                    break;
            }
            this.GetComponent<SceneByScene>().NextButtonPushed();
        }
    }

    public void EventPass()
    {
        this.GetComponent<SceneByScene>().NextButtonPushed();
    }

    public int GetEventNum()
    {
        return eventNum;
    }

    public void EventEnd()
    {
        descriptionText.text = "";
    }

    public void LanguageChange(int type)
    {
        languageType = type;
        descriptionText.text = event_Info.eventDescription[languageType, eventNum];
        if (event_Info.eventConditionType[eventNum] != 0)
            yesText.text = event_Info.eventConditionValue[eventNum] + (languageType == 0 ? "Use, " : " 소모, ") + event_Info.eventChoice[languageType, eventNum, 0];
        else
            yesText.text = event_Info.eventChoice[languageType, eventNum, 0];
        noText.text = event_Info.eventChoice[languageType, eventNum, 1];
    }
}
