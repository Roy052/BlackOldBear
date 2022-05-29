using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 target, mouse;
    float angle;
    public GameObject bear;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = bear.transform.position; // Aligning around the bear
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse Rotate
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
   	    this.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }
}
