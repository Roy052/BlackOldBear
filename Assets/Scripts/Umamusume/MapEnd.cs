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
    public bool gameEnd = false;
    GameObject sceneManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        sceneManagerObject = GameObject.FindGameObjectWithTag("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        Wolfpos = Wolf.transform.position;
        Bearpos = Bear.transform.position;

        
        if(gameEnd == false)
        {
            if (Wolfpos.x < 0 && Wolfpos.y >= 2)
            {
                WolfReach = true;
            }

            if (Bearpos.x < 0 && Bearpos.y >= 2)
            {
                BearReach = true;
            }

            if (WolfReach && !BearReach)
            {
                //lose
                Debug.Log("lose");
                sceneManagerObject.GetComponent<SceneByScene>().NextButtonON();
                gameEnd = true;
                GameObject.Find("EventManager").GetComponent<EventManager>().EventEnd();
            }
            if (!WolfReach && BearReach)
            {
                //win
                Debug.Log("win");
                sceneManagerObject.GetComponent<RewardManager>().RewardON();
                sceneManagerObject.GetComponent<SceneByScene>().NextButtonON();
                gameEnd = true;
                GameObject.Find("EventManager").GetComponent<EventManager>().EventEnd();
            }
        }

        
    }
}
