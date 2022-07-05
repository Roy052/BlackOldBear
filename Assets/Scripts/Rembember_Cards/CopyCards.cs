using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCards : MonoBehaviour
{
    public GameObject Card_Front;
    public GameObject Card_Back;
    Vector3 MousePosition;
    public Camera Camera;
    int i, j, k;

    List<GameObject> id_front = new List<GameObject>();
    List<GameObject> id_back = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        for (i = -8; i <= 8; i = i + 2)
        {
            for (j = -4; j <= 4; j = j + 2)
            {
                GameObject input_Front = Instantiate(Card_Front, new Vector3(i, j, -1), Quaternion.identity);
                GameObject input_Back = Instantiate(Card_Back, new Vector3(i, j, 0), Quaternion.identity);

                id_front.Add(input_Front);
                id_back.Add(input_Back);
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
                Debug.Log(hit.transform.gameObject);
            }
        }
    }
}
