using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockSet : MonoBehaviour
{

    public TextMeshProUGUI clockText;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 5;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        string timeText = (Mathf.Round(time * 10) / 10).ToString();
        clockText.text = timeText;

        if(time < 0)
        {
            time = 0;
        }
    }
}
