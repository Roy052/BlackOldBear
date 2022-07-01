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

    public int currentEvent;
    public Text descriptionText, yesText, noText;

    private void Start()
    {
        eventSceneList = new List<string>();
        eventSceneList.Add("Mini-AimBooster");
        eventSceneList.Add("RunningBear");
        
        eventNum = Random.Range(0, event_Info.eventDescription.Length);

        descriptionText.text = event_Info.eventDescription[eventNum];
        yesText.text = event_Info.eventChoice[eventNum, 0];
        noText.text = event_Info.eventChoice[eventNum, 1];
    }

    public void EventStart(int num)
    {
        if(num <= 1)
            SceneManager.LoadSceneAsync(eventSceneList[num], LoadSceneMode.Additive);
        else
        {
            switch (num)
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
