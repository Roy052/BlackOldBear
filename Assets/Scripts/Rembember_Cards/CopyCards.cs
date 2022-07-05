using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class CopyCards : MonoBehaviour
{
    public GameObject Card_Front;
    public GameObject Card_Back;
    Vector3 MousePosition;
    public Camera Camera;
    int i, j, k;
    int count = 0;
    List<GameObject> id_front = new List<GameObject>();
    List<GameObject> id_back = new List<GameObject>();

    List<bool> flip = new List<bool>();
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
                GameObject input_Back = Instantiate(Card_Back, new Vector3(i, j, 0), Quaternion.identity);

                input_Front.name = count.ToString();
                input_Back.name = count.ToString() + " Back";

                id_front.Add(input_Front);
                id_back.Add(input_Back);

                flip.Add(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);

            if (hit)
            {
                Debug.Log(hit.transform.gameObject.name);

                int now_id = Convert.ToInt32(hit.transform.gameObject.name.ToString());
                
                Debug.Log(mixed_backNumber[now_id]);
                flip[mixed_backNumber[now_id]] = true;
            }
        }
    }
}
