using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingKnife : MonoBehaviour
{
    private Vector3 ClickDaggerPosition;
    private Vector3 pos;
    private bool kill = false;
    public GameObject Knife;
    public GameObject Bear;
    public GameObject Wolf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickDaggerPosition = Input.mousePosition;
            ClickDaggerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ClickDaggerPosition, transform.forward, 15f);

            if (hit)
            {
                kill = true;
            }



        }

        if (kill)
        {
            transform.position = Wolf.transform.position;
            GameObject.Find("Wolf").GetComponent<MovingAnimal>().speed = 1;
        }

        
    }
}
