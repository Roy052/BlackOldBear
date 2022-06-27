using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimWolfCheck : MonoBehaviour
{

    public int count = 0;
    private Vector3 mouseposition;
    public TextMeshProUGUI countText;
    int countMax = 16;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseposition = Input.mousePosition;
            mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(mouseposition, transform.forward, 15f);

            if (hit)
            {

                count++;
                Debug.Log(count);
                Debug.Log(hit.transform.gameObject);

                hit.transform.position = new Vector3(-5, -9, 0);
            }
        }
        countText.text = count + " / " + countMax;
    }
}
