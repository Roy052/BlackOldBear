using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAnimal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private bool Flag1 = false;
    private bool Flag2 = false;
    private bool Flag3 = false;
    Vector3 destination1 = new Vector3(5.25f, -3.5f, 0.0f);
    Vector3 destination2 = new Vector3(5.25f, 3.7f, 0.0f);
    Vector3 destination3 = new Vector3(-1.25f, 3.7f, 0.0f);
    Vector3 new_Y;
    MapEnd mapEnd;
    private void Start()
    {
        mapEnd = GameObject.Find("MapEnd").GetComponent<MapEnd>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mapEnd.gameEnd == false)
        {
            if (transform.position == destination1)
            {
                Flag1 = true;
                new_Y = transform.position;
            }
            if (transform.position.y >= 3.7f)
            {
                Flag2 = true;
            }

            if (Flag1 == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination1, Time.deltaTime * speed);
            }
            else if (Flag1 && Flag2 == false)
            {
                new_Y += new Vector3(0, 1, 0) * Time.deltaTime * speed;
                float new_X = (Mathf.Sqrt(13.76f - transform.position.y * transform.position.y) + 5.25f) * 0.8f;
                transform.position = new Vector3(new_X, new_Y.y, 0);
                Debug.Log(new_X);

            }
            else if (Flag1 && Flag2 && Flag3 == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination3, Time.deltaTime * speed);

            }
        }
    }
}
