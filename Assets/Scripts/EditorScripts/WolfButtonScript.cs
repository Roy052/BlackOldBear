using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfButtonScript : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject lineManageObj;
    [HideInInspector] public int spriteIndex = 0;
    SpriteRenderer spRenderer;
    int spriteSize;
    LineManageScript lineManageScript;


    // Start is called before the first frame update
    void Start()
    {
        spriteSize = sprites.Count;
        spRenderer = GetComponent<SpriteRenderer>();
        lineManageScript = lineManageObj.GetComponent<LineManageScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        spriteIndex = (spriteIndex + 1) % spriteSize;
        spRenderer.sprite = sprites[spriteIndex];
        lineManageScript.wolfType = spriteIndex;
    }
}
