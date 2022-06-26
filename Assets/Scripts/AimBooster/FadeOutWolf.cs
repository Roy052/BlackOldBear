using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutWolf : MonoBehaviour
{
    Renderer renderer;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        renderer = target.GetComponent<Renderer>();
        Debug.Log("START");
    }

    IEnumerator FadeOut()
    {
        int i = 10;
        while(i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = renderer.sharedMaterial.color;
            c.a = f;
            renderer.sharedMaterial.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            StartCoroutine("FadeOut");
        }
    }
}
