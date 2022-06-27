using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StopWatch : MonoBehaviour
{
    private TextMeshProUGUI resourceText;
    public GameObject AimWolfCheck;
    private float time;
    public bool miniGameEnd = false;

    GameObject sceneManager;
    ObjectSpawner wolfSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        resourceText = GetComponent<TextMeshProUGUI>();
        resourceText.text = "Change";
        
        time = 30;

        wolfSpawner = GameObject.Find("WolfSpawner").GetComponent<ObjectSpawner>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(miniGameEnd == false)
        {
            time -= Time.deltaTime;
            string timeText = (Mathf.Round(time * 10) / 10).ToString();
            resourceText.text = timeText;
        }

        if(AimWolfCheck.GetComponent<AimWolfCheck>().count > 15)
        {
            sceneManager.GetComponent<SceneByScene>().RewardON();
            sceneManager.GetComponent<SceneByScene>().NextButtonON();

            miniGameEnd = true;
            wolfSpawner.miniGameEnd = true;
        }
        else if (time <= 0)
        {
            sceneManager.GetComponent<SceneByScene>().NextButtonON();

            miniGameEnd = true;
            wolfSpawner.miniGameEnd = true;
        }
    }

}
