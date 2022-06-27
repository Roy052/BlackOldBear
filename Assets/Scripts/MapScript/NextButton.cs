using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public SceneByScene sbs;
    private void Start()
    {
        sbs = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneByScene>();
    }
    private void OnMouseDown()
    {
        sbs.NextButtonPushed();
    }
}
