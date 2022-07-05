using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class CopyCards : MonoBehaviour
{
    public GameObject Card_Front;
    public GameObject Card_Back;
    public Camera Camera;
    int i, j, k;
    int count = 0;
    int First_Choice = -999;
    GameObject Before_GameObject;
    Vector3 MousePosition;

    int Left = 36;
    

    List<GameObject> id_front = new List<GameObject>();
    List<int> backNumber = new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
    List<int> mixed_backNumber = new List<int>();


    // Start is called before the first frame update
    void Start()
    {

        mixed_backNumber = backNumber.OrderBy(a => Guid.NewGuid()).ToList(); 

        for (i = -8; i <= 8; i = i + 2)
        {
            for (j = -3; j <= 4; j = j + 2)
            {
                count += 1;
                GameObject input_Front = Instantiate(Card_Front, new Vector3(i, j, -1), Quaternion.identity);

                input_Front.name = count.ToString();

                id_front.Add(input_Front);

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Left == 0)
        {
            //우승!!!
        }

        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);

            if (hit)
            {

                int now_id = Convert.ToInt32(hit.transform.gameObject.name.ToString());
                
                Debug.Log(mixed_backNumber[now_id-1]);

                if(First_Choice < 0){
                    First_Choice = mixed_backNumber[now_id-1];
                    Before_GameObject = hit.transform.gameObject;
                }
                else
                {
                    if(First_Choice == mixed_backNumber[now_id-1] && hit.transform.gameObject != Before_GameObject)
                    {
                        hit.transform.gameObject.GetComponent<SpriteRenderer>().material.color = Color.black;
                        Before_GameObject.GetComponent<SpriteRenderer>().material.color = Color.black;
                        Debug.Log("correct");
 //                       hit.transform.gameObject.GetComponent<SpriteRenderer>().material.color = Color.yellow;
                        First_Choice = -1;
                        Left = Left - 2;

                    }
                    else if(First_Choice == mixed_backNumber[now_id-1] && hit.transform.gameObject == Before_GameObject){
                        Debug.Log("click same cards");
                        First_Choice = -1;
                    }
                    else
                    {
                        Debug.Log("wrong");
                        First_Choice = -1;
                    }
                }
            }
        }
    }
}
