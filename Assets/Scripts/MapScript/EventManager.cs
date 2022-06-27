using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    List<string> eventList;

    private void Start()
    {
        eventList = new List<string>();
        eventList.Add("Mini-AimBooster");

        SceneManager.LoadSceneAsync(eventList[Random.Range(0, eventList.Count)], LoadSceneMode.Additive);
    }
}
