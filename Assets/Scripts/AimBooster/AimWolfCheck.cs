using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWolfCheck : MonoBehaviour
{
    Renderer renderer;
    public GameObject target;

    private int count = 0;
    private Vector3 mouseposition;

    // Start is called before the first frame update
    void Start()
    {
        renderer = target.GetComponent<Renderer>();
        Debug.Log("START");
    }

    IEnumerator FadeOut()
    {
        int i = 10;
        while (i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = renderer.sharedMaterial.color;
            c.a = f;
            renderer.sharedMaterial.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator FadeIn()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
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

        if (Input.GetMouseButtonDown(0))
        {
            mouseposition = Input.mousePosition;
            mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(mouseposition, transform.forward, 15f);

            if (hit)
            {
                count++;
                Debug.Log(count);
                StartCoroutine("FadeIn");
            }
        }
    }
}
