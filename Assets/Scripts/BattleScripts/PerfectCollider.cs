using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCollider : MonoBehaviour
{
    GameManager gm;
    float arr;
    float now;
    void Start()
    {
        arr = 3;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        arr *= gm.perfectSize;

        now = transform.localScale.x;

        transform.localScale = new Vector3(arr, arr, 1);
    }
}
