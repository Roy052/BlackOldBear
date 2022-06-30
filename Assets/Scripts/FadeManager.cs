using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public GameObject fade;
    SpriteRenderer fadeSpriteRenderer;

    public IEnumerator FadeIn(float timeLength)
    {
        fadeSpriteRenderer = fade.GetComponent<SpriteRenderer>();
        Color color = fadeSpriteRenderer.color;

        while (color.a > 0)
        {
            color.a -= Time.fixedDeltaTime / timeLength;
            fadeSpriteRenderer.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator FadeOut(float timeLength)
    {
        fadeSpriteRenderer = fade.GetComponent<SpriteRenderer>();
        Color color = fadeSpriteRenderer.color;

        while (color.a < 1)
        {
            color.a += Time.fixedDeltaTime / timeLength;
            fadeSpriteRenderer.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    static public IEnumerator FadeIn(SpriteRenderer spriteRenderer, float timeLength)
    {

        Color color = spriteRenderer.color;

        while (color.a > 0)
        {
            color.a -= Time.fixedDeltaTime / timeLength;
            spriteRenderer.color = color;
            yield return new WaitForFixedUpdate();
        }
    }
    static public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float timeLength)
    {
        Color color = spriteRenderer.color;

        while (color.a < 1)
        {
            color.a += Time.fixedDeltaTime / timeLength;
            spriteRenderer.color = color;
            yield return new WaitForFixedUpdate();
        }
    }
}
