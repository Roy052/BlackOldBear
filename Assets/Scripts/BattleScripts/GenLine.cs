using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenLine : MonoBehaviour
{
    public GameObject dot;
    public GameObject bear;
    WolfManager wm;
    // Start is called before the first frame update
    public float distance = 1.0f; // radius
    Vector3 center; //bear pos
    float x, y;

    void Start()
    {
        wm = GameObject.Find("Wolfs").GetComponent<WolfManager>();
        center = bear.transform.position;
        for(int i=0;i<360;i+=10)
        {
            x = (Mathf.Cos(i*Mathf.Deg2Rad)+center.x)*distance;
            y = (Mathf.Sin(i*Mathf.Deg2Rad)+center.y)*distance;
            Instantiate(dot,new Vector3(x,y,0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wolf")
            Debug.Log(wm.time_current);
    }
}
