using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wolf : MonoBehaviour
{
    public int judgementState = 0; // 0 : out, 1 : bad, 2 : great, 3 : perfect
    public int difficultly = 3;
    bool isBad = false;
    bool isMoved = false;

    GameManager gm;
    WolfManager wm;
    PlayerController pc;
    BattleManager bm;
    JudgeManager jm;
    StatusManager sm;

    Vector3 bearPosition;
    float distance; // to Bear(center)
    public Vector3 direction; // to Bear(center)
    float latency;

    SpriteRenderer sr;
    //score
    float type = 0.0f;
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
    float currTime,enterTime;

    public int wolfHP;
    public enum WolfState {die, idle, damaged};
    public WolfState wolfState = WolfState.idle;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        wm = GameObject.Find("Wolfs").GetComponent<WolfManager>();
        pc = GameObject.Find("Mouse Director").GetComponent<PlayerController>();
        bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        jm = GameObject.Find("JudgeEffect").GetComponent<JudgeManager>();
        sm = GameObject.Find("GameManager").GetComponent<StatusManager>();

        setScore();
        setAngle();
        currTime = Time.time;
        enterTime = Time.time;
        bearPosition = wm.getBearPosition();

        Vector3 temp = bearPosition - this.transform.position; // set direction
        distance = temp.magnitude;
        direction = temp / distance;
        direction = direction.normalized;
        latency = gm.speed*-0.2f + 2.2f; // set Note Speed

        wolfHP = wm.wolfHp[wm.now];
        type = wm.wolfTyped[wm.now++];
    }
    // Update is called once per frame
    void Update()
    {
        if (!isMoved)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!wm.clicked)
                {
                    procNote();
                    if (!isBad && wm.wolfGenerated[wm.first - 1] == gameObject)
                    {
                        wolfHP--;
                        if (wolfHP > 0)
                        {
                            isDistroyed = false;
                            wolfState = WolfState.damaged;
                            isMoved = true;
                            wm.emptyFirst++;
                            wm.first--;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isDistroyed)
                {
                    if (isBad && wm.wolfGenerated[wm.first-1] == gameObject)
                    {
                        wm.emptyFirst += wolfHP-1;
                    }
                    Destroy(gameObject);
                }
                isBad = false;
                wm.clicked = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!wm.clicked)
                {
                    procNote();
                    if (!isBad&& wm.wolfGenerated[wm.first-1] == gameObject)
                    {
                        wolfHP--;
                        if (wolfHP > 0)
                        {
                            isDistroyed = false;
                            wolfState = WolfState.damaged;
                            isMoved = true;
                            wm.emptyFirst++;
                            wm.first--;
                        }
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (isDistroyed)
                {
                    if (isBad && wm.wolfGenerated[wm.first-1] == gameObject)
                    {
                        wm.emptyFirst += wolfHP-1;
                    }
                    Destroy(gameObject);
                }
                isBad = false;
                wm.clicked = false;
            }
        }

        if (wolfState == WolfState.damaged)
        {
            if(isMoved)
            {
                wm.clicked = true;
                try
                {
                    if (wm.wolfEmpty[wm.emptyFirst] == null)
                    {
                        isMoved = false;
                        //wm.clicked = false;
                        //wolfState = WolfState.idle;
                        direction = (-this.transform.position).normalized;
                    }

                else
                {
                    if ((wm.wolfEmpty[wm.emptyFirst].transform.position - this.transform.localPosition).magnitude < 0.1)
                    {
                            direction = (bearPosition - this.transform.position).normalized;
                            isMoved = false;
                            wm.clicked = false;
                        //wolfState = WolfState.idle;
                        type = wm.wolfEmpty[wm.emptyFirst].GetComponent<EmptyWolf>().type;
                    }
                    direction = (wm.wolfEmpty[wm.emptyFirst].transform.position - this.transform.position).normalized * 2;
                }
                transform.position += direction * Time.deltaTime * (distance / latency);
                }
                catch
                {
                    Debug.Log(wm.wolfEmpty.Count+" "+wm.emptyFirst);
                }
            }
            else
            {
                //wm.clicked = false;
                if (type == 0.0f)
                {
                    direction= (bearPosition - this.transform.position).normalized; // set direction
                    transform.position += direction * Time.deltaTime * (distance / latency);
                }
                else
                {
                    direction = (bearPosition - this.transform.position).normalized; // set direction
                    transform.position += direction * Time.deltaTime * (distance / latency);
                    transform.RotateAround(bearPosition, Vector3.back, Time.deltaTime * 70);
                    transform.Rotate(Vector3.forward, Time.deltaTime * 70);
                }
            }

            //catch
            {
                //.Log(wm.wolfEmpty.Count + " " + wm.emptyFirst + " " + wm.wolfGenerated.Count + " " + wm.first);
            }
        }
        else if (type == 0.0f)
        {
            transform.position += direction * Time.deltaTime * (distance / latency);
        }
        else
        {
            direction = (bearPosition - this.transform.position).normalized; // set direction
            transform.position += direction * Time.deltaTime * (distance / latency);
            transform.RotateAround(bearPosition, Vector3.back, Time.deltaTime * 70);
            transform.Rotate(Vector3.forward, Time.deltaTime * 70);
        }

        sr = GetComponent<SpriteRenderer>();
        if (transform.position.x > 0)
            sr.flipX = true;
        else
            sr.flipX = false;
    }

    float cosineDistance()
    {
        float angle = pc.angle;
        Vector3 check_direction = bearPosition - this.transform.position;
        Vector3 mDirection = new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        return Vector3.Dot(mDirection, check_direction)/mDirection.magnitude/check_direction.magnitude;
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
            Debug.Log("bad1");
            gm.score += scB;
            wm.first++;
            sm.ChangeHealth(-(wolfHP + difficultly));
            jm.setJudgeImage(1);
            jm.playJudgeSound(1);
            bm.countJudge(1);
            isDistroyed = true;
            isBad = true;
            // Destroy(gameObject);
        }
        else if (judgementState == 2) // great
        {
            if (cosDis > anglePf) // perfect
            {
                gm.score += scGP;
                wm.first++;
                jm.setJudgeImage(2);
                jm.playJudgeSound(2);
                bm.countJudge(2);
                isDistroyed = true;
                isBad = false;
                // Destroy(gameObject);
            }
            else if (cosDis > angleGr) // great
            {
                gm.score += scGG;
                wm.first++;
                jm.setJudgeImage(2);
                jm.playJudgeSound(2);
                bm.countJudge(2);
                isDistroyed = true;
                isBad = false;
                // Destroy(gameObject);
            }
            else // bad
            {
                Debug.Log("bad2");
                gm.score += scGB;
                wm.first++;
                sm.ChangeHealth(-(wolfHP + difficultly));
                jm.setJudgeImage(1);
                jm.playJudgeSound(1);
                bm.countJudge(1);
                isDistroyed = true;
                isBad = true;
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
                jm.playJudgeSound(3);
                bm.countJudge(3);
                isDistroyed = true;
                isBad = false;
                // Destroy(gameObject);
            }
            else if (cosDis > angleGr) // great
            {
                gm.score += scPG;
                wm.first++;
                jm.setJudgeImage(2);
                jm.playJudgeSound(2);
                bm.countJudge(2);
                isDistroyed = true;
                isBad = false;
                // Destroy(gameObject);
            }
            else // bad
            {
                Debug.Log("bad3");
                gm.score += scPB;
                wm.first++;
                sm.ChangeHealth(-(wolfHP + difficultly));
                jm.setJudgeImage(1);
                jm.playJudgeSound(1);
                bm.countJudge(1);
                isDistroyed = true;
                isBad = true;
                // Destroy(gameObject);
            }
        } 
    }
    float check;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bad"&&!isMoved)
        {
            float time = Time.time;
            //Debug.Log("exit time " + (time - currTime));
            //Debug.Log("mid time " + (check + time - currTime) / 2);
            currTime = time;

            if (!isDistroyed)
            {
                Debug.Log("bad4"+wm.clicked);
                judgementState = 1;
                gm.score += scB;
                wm.first++;
                sm.ChangeHealth(-(wolfHP + difficultly));
                wm.emptyFirst += (wolfHP-1);
                jm.setJudgeImage(1);
                jm.playJudgeSound(1);
                bm.countJudge(1);
                // Debug.Log("Bad in");
                Destroy(gameObject);
            }

            // Debug.Log("Bad in");
        }
        else if(collision.tag=="Great")
        {
            judgementState = 2;
            // Debug.Log("Great in");
        }
        else if(collision.tag=="Perfect")
        {
            float time = Time.time;
            //Debug.Log("enter time " + (time - enterTime));
            check = time - enterTime;
            enterTime = time;
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
    }
}