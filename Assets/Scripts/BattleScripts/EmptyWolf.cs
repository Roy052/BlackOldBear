using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EmptyWolf : MonoBehaviour
{
    public int judgementState = 0; // 0 : out, 1 : bad, 2 : great, 3 : perfect

    GameManager gm;
    WolfManager wm;

    Vector3 bearPosition;
    float distance; // to Bear(center)
    public Vector3 direction; // to Bear(center)
    float latency;

    private int wolfHP;
    public enum WolfState { die, idle, damaged };
    public WolfState wolfState = WolfState.idle;
    public float type;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        wm = GameObject.Find("Wolfs").GetComponent<WolfManager>();

        bearPosition = wm.getBearPosition();

        Vector3 temp = bearPosition - this.transform.position; // set direction
        distance = temp.magnitude;
        direction = temp / distance;
        direction = direction.normalized;
        latency = gm.speed * -0.2f + 2.2f; // set Note Speed

        wolfHP = wm.wolfHp[wm.now];
        type = wm.wolfTyped[wm.now++];
    }
    // Update is called once per frame
    void Update()
    {
        if (type == 0.0f)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad")
        {
            if((transform.position-bearPosition).magnitude<0.1)
                Destroy(gameObject);
        }
    }
}