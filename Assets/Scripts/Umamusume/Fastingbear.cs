using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fastingbear : MonoBehaviour
{
    Vector3 pos;
    public float Bearspeed;

    bool Flag1 = false;
    bool Flag2 = false;
    bool Flag3 = false;
    bool Flag4 = false;
    bool Flag5 = false;

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
            if (pos.x > 5.4)
            {
                Flag1 = true;
            }
            if (pos.x >= 8.6)
            {
                Flag2 = true;
            }
            if (pos.y > 0)
            {
                Flag3 = true;
            }
            if (pos.x <= 8.61 && pos.y > 0)
            {
                Flag4 = true;
            }
            if (pos.y >= 4.8)
            {
                Flag5 = true;
            }


            if (!Flag1)
            {
                transform.position = new Vector3(pos.x + Bearspeed, pos.y, 0.0f);
            }
            else if (Flag1 && !Flag2 && !Flag3 && !Flag4 && !Flag5)
            {
                transform.position = new Vector3(pos.x + Bearspeed, pos.y + Bearspeed * 0.8f, 0.0f);
            }
            else if (Flag1 && Flag2 && !Flag3 && !Flag4 && !Flag5)
            {
                transform.position = new Vector3(pos.x + Bearspeed * 0.2f, pos.y + Bearspeed * 1.2f, 0.0f);

            }
            else if (Flag1 && Flag2 && Flag3 && !Flag4 && !Flag5)
            {
                transform.position = new Vector3(pos.x - Bearspeed * 0.2f, pos.y + Bearspeed * 1.2f, 0.0f);
            }
            else if (Flag1 && Flag2 && Flag3 && Flag4 && !Flag5)
            {
                transform.position = new Vector3(pos.x - Bearspeed, pos.y + Bearspeed * 0.8f, 0.0f);
            }
            else if (Flag1 && Flag2 && Flag3 && Flag4 && Flag5)
            {
                transform.position = new Vector3(pos.x - Bearspeed, pos.y, 0.0f);

            }
        }
    }
}
