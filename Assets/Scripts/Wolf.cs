using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public int judgementState = 0; // 0 : out, 1 : bad, 2 : great, 3 : perfect
    GameManager gm;
    WolfManager wm;

    Vector3 bearPosition;
    float distance; // to Bear(center)
    Vector3 direction; // to Bear(center)
    float Speed = 1.0f;

    bool isDistroyed = false;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        wm = GameObject.Find("Wolfs").GetComponent<WolfManager>();
        bearPosition = wm.getBearPosition();

        Vector3 temp = bearPosition - this.transform.position; // set direction
        distance = temp.magnitude;
        direction = temp / distance;

        Speed = gm.speed; // set Note Speed
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * Time.deltaTime * Speed;
        if (Input.GetMouseButtonDown(0))
        {
            if(!wm.clicked)
            {
                procNote();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(isDistroyed)
                Destroy(gameObject);
            wm.clicked = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            procNote();
        }
    }

    void procNote()
    {
        if(wm.wolfGenerated[wm.first]==gameObject)
        {
            wm.clicked = true;
        }

        // Debug.Log("processing Note");
        if (judgementState == 1) // Bad
        {
            gm.score -= 10;
            wm.first++;
            isDistroyed = true;
            // Destroy(gameObject);
        } 
        else if (judgementState == 2) // great
        {
            gm.score += 5;
            wm.first++;
            isDistroyed = true;
            // Destroy(gameObject);
        }
        else if (judgementState == 3) // perfect
        {
            gm.score += 10;
            wm.first++;
            isDistroyed = true;
            // Destroy(gameObject);
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bad")
        {
            judgementState = 1;
            // Debug.Log("Bad in");
        }
        else if(collision.tag=="Great")
        {
            judgementState = 2;
            // Debug.Log("Great in");
        }
        else if(collision.tag=="Perfect")
        {
            judgementState = 3;
            // Debug.Log("Perfect in");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Perfect")
        {
            judgementState = 2;
            // Debug.Log("Perfect out");
        }
        else if(collision.tag=="Great")
        {
            judgementState = 1;
            // Debug.Log("Great out");
        }
        else if(collision.tag=="Bad")
        {
            if(!isDistroyed)
            {
                judgementState = 0;
                gm.score -= 10;
                wm.first++;
                // Debug.Log("Bad out");
                Destroy(gameObject);
            }
        }
    }
}
