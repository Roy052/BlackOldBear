using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSize : MonoBehaviour
{
    GameManager gm;
    float[] arr;
    int level;
    float now;
    void Start()
    {
        arr = new float[5];
        arr[0] = 3;
        arr[1] = 3.5f;
        arr[2] = 4;
        arr[3] = 4.5f;
        arr[4] = 5;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        level = gm.perfectSize;

        now = transform.localScale.x;
        transform.localScale = new Vector3(now/4*arr[level],now/4*arr[level],1);
    }
}
