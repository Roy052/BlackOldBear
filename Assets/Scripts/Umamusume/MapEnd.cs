using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnd : MonoBehaviour { 

    private bool WolfReach = false;
    private bool BearReach = false;

    private Vector3 Wolfpos;
    private Vector3 Bearpos;

    public GameObject Wolf;
    public GameObject Bear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wolfpos = Wolf.transform.position;
        Bearpos = Bear.transform.position;

        if (Wolfpos.x > 7.5)
        {
            WolfReach = true;
        }

        if (Bearpos.x > 7.5)
        {
            BearReach = true;
        }

        if(WolfReach && BearReach)
        {
            //lose
            Debug.Log("lose");
        }
        if(!WolfReach && BearReach)
        {
            //win
            Debug.Log("win");
        }
    }
}
