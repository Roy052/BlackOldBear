using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAnimal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Vector3 destination = new Vector3(9.0f, 2.0f, 0.0f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
    }
}
