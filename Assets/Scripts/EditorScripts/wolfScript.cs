using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfScript : MonoBehaviour
{
    public float beat;
    public float angle;
    public LineManageScript lineManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
