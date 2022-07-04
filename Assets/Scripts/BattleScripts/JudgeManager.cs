using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public Sprite perfect;
    public Sprite great;
    public Sprite bad;

    public AudioClip perfectSound;
    public AudioClip greatSound;
    public AudioClip badSound;

    public AudioSource audioSource;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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

    public void playJudgeSound(int state)
    {
        switch(state)
        {
            case 1:
                audioSource.clip = badSound;
                break;
            case 2:
                audioSource.clip = greatSound;
                break;
            case 3:
                audioSource.clip = perfectSound;
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
