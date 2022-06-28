using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fastingbear : MonoBehaviour
{
    Vector3 pos;
    public float Bearspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pos = transform.position;
            transform.position = new Vector3(pos.x+Bearspeed, pos.y, 0.0f);
            Debug.Log("Move!");
        }
    }
}
