using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSM : MonoBehaviour
{
    // Start is called before the first frame update

    public Text score;
    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score : " + gm.score;
    }
}
