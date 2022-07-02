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

    private void Start()
    {
        eventSceneList = new List<string>();
        eventSceneList.Add("Mini-AimBooster");
        eventSceneList.Add("RunningBear");
        gameManagerObject = GameObject.Find("GameManager");

        eventNum = Random.Range(0, event_Info.eventDescription.Length);

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
        if(eventNum <= 1)
            SceneManager.LoadSceneAsync(eventSceneList[eventNum], LoadSceneMode.Additive);
        else
        {
            switch (eventNum)
            {
                case 2:
                    Debug.Log("재료 주세요");
                    break;
            }
        }
            
    }

    public void EventPass()
    {
        this.GetComponent<SceneByScene>().NextButtonPushed();
    }
}
