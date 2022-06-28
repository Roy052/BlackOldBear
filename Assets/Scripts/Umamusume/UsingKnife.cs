using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingKnife : MonoBehaviour
{
    private Vector3 ClickDaggerPosition;
    private Vector3 pos;
    private GameObject copyKnife;
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
                Debug.Log("Dagging");
   //             transform.position = new Vector3(-20, 20, 0); //치우기

//                copyKnife = Instantiate(Knife,Bear.transform.position,Quaternion.identity);
//                copyKnife.transform.position = Vector3.MoveTowards(Bear.transform.position, Wolf.transform.position, 3);
//                if (copyKnife.transform.position == Wolf.transform.position)
//                {
//                    Destroy(copyKnife);
//                }
            }
        }
    }
}
