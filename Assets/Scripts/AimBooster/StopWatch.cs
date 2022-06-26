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
    // Start is called before the first frame update
    void Start()
    {
        resourceText = GetComponent<TextMeshProUGUI>();
        resourceText.text = "Change";
        time = 30;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        resourceText.text = time.ToString();

        if(AimWolfCheck.GetComponent<AimWolfCheck>().count > 15)
        {
            SceneManager.LoadScene("MapScene", LoadSceneMode.Single);

        }
        else if (time <= 0)
        {
            SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
        }
    }

}
