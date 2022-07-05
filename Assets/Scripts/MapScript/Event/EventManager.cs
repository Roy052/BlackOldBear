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
    private void Start()
    {
        eventSceneList = new List<string>();
        eventSceneList.Add("Mini-AimBooster");
        eventSceneList.Add("RunningBear");
        gameManagerObject = GameObject.Find("GameManager");

        //eventNum = Random.Range(0, event_Info.eventDescription.Length);
        eventNum = 0;

        backroundImage.sprite = backgroundImageSprites[eventNum];
        descriptionText.text = event_Info.eventDescription[eventNum];
        Debug.Log(eventNum);
        Debug.Log(event_Info.eventConditionType[eventNum]);
        
        if(event_Info.eventConditionType[eventNum] != 0)
        {
            switch (event_Info.eventConditionType[eventNum])
            {
                case 1:
                    Debug.Log(gameManagerObject.GetComponent<ItemManager>().currentMoney());
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
            yesText.text = event_Info.eventConditionValue[eventNum] + " 소모, " + event_Info.eventChoice[eventNum, 0];
        }
        else
        {
            yesText.text = event_Info.eventChoice[eventNum, 0];
        }
        noText.text = event_Info.eventChoice[eventNum, 1];
        
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
        }
            
        else
        {
            //Reward
            switch (eventNum)
            {
                case 2:
                    gameManagerObject.GetComponent<ItemManager>().itemChange(0, 3);
                    break;
                case 3:
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
}
