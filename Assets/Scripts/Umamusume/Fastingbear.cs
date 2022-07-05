using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fastingbear : MonoBehaviour
{
    Vector3 pos;
    public float Bearspeed;
    MapEnd mapEnd;

    bool Flag1 = false;
    bool Flag2 = false;
    bool Flag3 = false;
    bool Flag4 = false;
    bool Flag5 = false;

    private void Start()
    {
        mapEnd = GameObject.Find("MapEnd").GetComponent<MapEnd>();
    }
    void Update()
    {
        if(mapEnd.gameEnd == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pos = transform.position;
                if (pos.x >= 5)
                {
                    Flag1 = true;
                }
                if (pos.x >= 8.2)
                {
                    Flag2 = true;
                }
                if (pos.y >= -0.3)
                {
                    Flag3 = true;
                }
                if (pos.x <= 8.2 && pos.y >= -0.3)
                {
                    Flag4 = true;
                }
                if (pos.y >= 3.6)
                {
                    Flag5 = true;
                }


                if (!Flag1)
                {
                    transform.position = new Vector3(pos.x + Bearspeed, pos.y, 0.0f);
                }
                else if (Flag1 && !Flag2 && !Flag3 && !Flag4 && !Flag5)
                {
                    transform.position = new Vector3(pos.x + Bearspeed, pos.y + Bearspeed * 0.7f, 0.0f);
                }
                else if (Flag1 && Flag2 && !Flag3 && !Flag4 && !Flag5)
                {
                    transform.position = new Vector3(pos.x + Bearspeed * 0.2f, pos.y + Bearspeed * 1.2f, 0.0f);

                }
                else if (Flag1 && Flag2 && Flag3 && !Flag4 && !Flag5)
                {
                    transform.position = new Vector3(pos.x - Bearspeed * 0.3f, pos.y + Bearspeed * 1.2f, 0.0f);
                }
                else if (Flag1 && Flag2 && Flag3 && Flag4 && !Flag5)
                {
                    transform.position = new Vector3(pos.x - Bearspeed, pos.y + Bearspeed * 0.7f, 0.0f);
                }
                else if (Flag1 && Flag2 && Flag3 && Flag4 && Flag5)
                {
                    transform.position = new Vector3(pos.x - Bearspeed, pos.y, 0.0f);

                }
            }
        }
    }
}
