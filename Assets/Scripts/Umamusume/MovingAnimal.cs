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
    Vector3 destination2 = new Vector3(5.25f, 3.5f, 0.0f);
    Vector3 destination3 = new Vector3(-1.25f, 3.7f, 0.0f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == destination1){
            Flag1 = true;
        }
        if (transform.position.y > 3.4f)
        {
            Flag2 = true;
        }

        if (Flag1 == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination1, Time.deltaTime * speed);
        }
        else if(Flag1 && Flag2 == false)
        {
            transform.position = Vector3.Slerp(transform.position, destination2, Time.deltaTime * speed);

        }
        else if(Flag1 && Flag2 && Flag3 == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination3, Time.deltaTime * speed);

        }


    }
}
