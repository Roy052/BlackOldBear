using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCollider : MonoBehaviour
{
    GameManager gm;
    float[] arr;
    int level;
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

        transform.localScale = new Vector3(arr[level],arr[level],1);
    }
}
