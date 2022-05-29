using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Color lineColor;
    public float xstart;
    public float ystart;
    public float zstart;
    public float angle;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;
        line.useWorldSpace = false;

        float xend = Mathf.Cos(Mathf.Deg2Rad * angle) * 10;
        float yend = Mathf.Sin(Mathf.Deg2Rad * angle) * 10;

        line.SetPosition(0, new Vector3(xstart, ystart, zstart));
        line.SetPosition(1, new Vector3(xstart + xend, ystart + yend, zstart));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
