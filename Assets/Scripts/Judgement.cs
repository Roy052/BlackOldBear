using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public int judgeNum;
    public PlayerController pc;
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pc.judgementState = judgeNum;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pc.judgementState = 0;
    }

}
