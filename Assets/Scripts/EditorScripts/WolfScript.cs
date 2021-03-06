using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfScript : MonoBehaviour
{
    public int node;
    public int fullBeat;
    public int beat;
    public float angle;
    public LineManageScript lineManagerScript;
    public int type;
    SpriteRenderer sprend;

    // Start is called before the first frame update
    void Start()
    {
        sprend = GetComponent<SpriteRenderer>();
        if (transform.position.x > 0)
            sprend.flipX = true;
        else
            sprend.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            lineManagerScript.wolfRemove(this);
            Destroy(this.gameObject);
        }
    }
}
