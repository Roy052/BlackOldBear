using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public MapManager mm;
    public int mapIconNum;
    public Vector2 position;
    public Sprite revealIcon;
    public Sprite[] sealedIcon;
    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if(mm.Visitable((int) position.x, (int) position.y))
            mm.SceneMovement(mapIconNum, position);
    }
}
