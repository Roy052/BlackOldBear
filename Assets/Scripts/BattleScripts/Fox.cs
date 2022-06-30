using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fox : MonoBehaviour
{
    public int judgementState = 0; // 0 : out, 1 : bad, 2 : great, 3 : perfect
    GameManager gm;
    WolfManager wm;
    PlayerController pc;
    BattleManager bm;
    JudgeManager jm;

    Vector3 bearPosition;
    float distance; // to Bear(center)
    public Vector3 direction; // to Bear(center)
    float latency;
    float curveAngle;
    //score
    float anglePf = 0.97f;
    float angleGr = 0.85f;
    int scPP = 10;
    int scPG = 7;
    int scPB = 1;
    int scGP = 5;
    int scGG = 3;
    int scGB = -1;
    int scB = -10;
    bool isDistroyed = false;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        wm = GameObject.Find("Wolfs").GetComponent<WolfManager>();
        pc = GameObject.Find("Mouse Director").GetComponent<PlayerController>();
        bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        jm = GameObject.Find("JudgeEffect").GetComponent<JudgeManager>();

        setScore();
        setAngle();

        bearPosition = wm.getBearPosition();

        latency = gm.speed * -0.2f + 2.2f; // set Note Speed
        Vector3 temp = bearPosition - this.transform.position; // set direction
        //Vector3 temp = bearPosition - Quaternion.Euler(0,0, -curveAngle*0.7f) * this.transform.position; // set direction
        distance = temp.magnitude;
        direction = temp / distance;
        direction = direction.normalized;
        
    }
    // Update is called once per frame
    void Update()
    {
        direction = (bearPosition - this.transform.position).normalized; // set direction
        transform.position += direction * Time.deltaTime * (distance / latency);
        transform.RotateAround(bearPosition, Vector3.back, Time.deltaTime * curveAngle);
        transform.Rotate(Vector3.forward,Time.deltaTime*curveAngle);
        if (Input.GetMouseButtonDown(0))
        {
            if (!wm.clicked)
            {
                procNote();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDistroyed)
                Destroy(gameObject);
            wm.clicked = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!wm.clicked)
            {
                procNote();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isDistroyed)
                Destroy(gameObject);
            wm.clicked = false;
        }
    }

    float cosineDistance()
    {
        float angle = pc.angle;
        Vector3 mDirection = new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        return Vector3.Dot(mDirection, direction) / mDirection.magnitude / direction.magnitude;
    }

    void procNote()
    {
        float cosDis = cosineDistance();

        if (wm.wolfGenerated[wm.first] == gameObject)
        {
            wm.clicked = true;
        }

        // Debug.Log("processing Note");
        if (judgementState == 1) // Bad
        {
            gm.score += scB;
            wm.first++;
            jm.setJudgeImage(1);
            isDistroyed = true;
            // Destroy(gameObject);
        }
        else if (judgementState == 2) // great
        {
            if (cosDis > anglePf) // perfect
            {
                gm.score += scGP;
                wm.first++;
                jm.setJudgeImage(2);
                isDistroyed = true;
                // Destroy(gameObject);
            }
            else if (cosDis > angleGr) // great
            {
                gm.score += scGG;
                wm.first++;
                jm.setJudgeImage(2);
                isDistroyed = true;
                // Destroy(gameObject);
            }
            else // bad
            {
                gm.score += scGB;
                wm.first++;
                jm.setJudgeImage(1);
                isDistroyed = true;
                // Destroy(gameObject);
            }
        }
        else if (judgementState == 3) // perfect
        {
            if (cosDis > anglePf) // perfect
            {
                gm.score += scPP;
                wm.first++;
                jm.setJudgeImage(3);
                isDistroyed = true;
                // Destroy(gameObject);
            }
            else if (cosDis > angleGr) // great
            {
                gm.score += scPG;
                wm.first++;
                jm.setJudgeImage(3);
                isDistroyed = true;
                // Destroy(gameObject);
            }
            else // bad
            {
                gm.score += scPB;
                wm.first++;
                jm.setJudgeImage(1);
                isDistroyed = true;
                // Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad")
        {
            if (!isDistroyed)
            {
                judgementState = 1;
                gm.score += scB;
                wm.first++;
                jm.setJudgeImage(1);
                // Debug.Log("Bad in");
                Destroy(gameObject);
            }
            // Debug.Log("Bad in");
        }
        else if (collision.tag == "Great")
        {
            judgementState = 2;
            // Debug.Log("Great in");
        }
        else if (collision.tag == "Perfect")
        {
            judgementState = 3;
            // Debug.Log("Perfect in");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Perfect")
        {
            judgementState = 2;
            // Debug.Log("Perfect out");
        }
        else if (collision.tag == "Great")
        {
            judgementState = 1;
            // Debug.Log("Great out");
        }
        else if (collision.tag == "Bad")
        {
            judgementState = 0;
        }
    }

    private void setScore()
    {
        scPP = bm.scPP;
        scPG = bm.scPG;
        scPB = bm.scPB;
        scGP = bm.scGP;
        scGG = bm.scGG;
        scGB = bm.scGB;
        scB = bm.scB;
    }

    private void setAngle()
    {
        anglePf = bm.anglePf;
        angleGr = bm.angleGr;
        curveAngle = wm.curveAngle;
    }
}