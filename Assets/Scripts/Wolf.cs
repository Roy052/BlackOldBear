using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float Speed = 1.0f;
    public int judgementState = 0; // 0 : out, 1 : bad, 2 : great, 3 : perfect
    GameManager gm;

 
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(1, 0) * Time.deltaTime * Speed;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            if (judgementState == 1) // Bad
            {
                gm.score -= 10;
                Debug.Log("Click on bad");
                Destroy(gameObject);
            } 
            else if (judgementState == 2) // great
            {
                gm.score += 5;
                Debug.Log("Click on great");
                Destroy(gameObject);
            }
            else if (judgementState == 3) // perfect
            {
                gm.score += 10;
                Debug.Log("Click on perfect");
                Destroy(gameObject);
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bad")
        {
            judgementState = 1;
            Debug.Log("Bad in");
        }
        else if(collision.tag=="Great")
        {
            judgementState = 2;
            Debug.Log("Great in");
        }
        else if(collision.tag=="Perfect")
        {
            judgementState = 3;
            Debug.Log("Perfect in");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Perfect")
        {
            judgementState = 2;
            Debug.Log("Perfect out");
        }
        else if(collision.tag=="Great")
        {
            judgementState = 1;
            Debug.Log("Great out");
        }
        else if(collision.tag=="Bad")
        {
            judgementState = 0;
            Debug.Log("Bad out");
            Destroy(gameObject);
        }
    }

}
