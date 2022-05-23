using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int judgementState = 0; //0 : Miss, 1 : Perfect, 2 : Great
    GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (judgementState == 0) gm.score -= 10;
            else if (judgementState == 1) gm.score += 10;
            else gm.score += 5;

        }
    }
}
