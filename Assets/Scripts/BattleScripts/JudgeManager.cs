using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public Sprite perfect;
    public Sprite great;
    public Sprite bad;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setJudgeImage(int state)
    {
        switch(state)
        {
            case 1:
                sr.sprite = bad;
                break;
            case 2:
                sr.sprite = great;
                break;
            case 3:
                sr.sprite = perfect;
                break;
            default:
                break;
        }
    }
}
