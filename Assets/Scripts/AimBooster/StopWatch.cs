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
    public GameObject mainCamera;

    GameObject sceneManager;
    ObjectSpawner wolfSpawner;
    bool onetime = false; //한번만 실행하기 위함
    
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

        if(onetime == false && AimWolfCheck.GetComponent<AimWolfCheck>().count > 15)
        {
            sceneManager.GetComponent<SceneByScene>().RewardON();
            sceneManager.GetComponent<SceneByScene>().NextButtonON();
            mainCamera.SetActive(false);
            MiniGameEnd();
        }
        else if (onetime == false && time <= 0)
        {
            sceneManager.GetComponent<SceneByScene>().NextButtonON();
            mainCamera.SetActive(false);
            MiniGameEnd();
        }
    }

    void MiniGameEnd()
    {
        miniGameEnd = true;
        wolfSpawner.miniGameEnd = true;
        wolfSpawner.Arrangement();

        onetime = true;
    }

}
