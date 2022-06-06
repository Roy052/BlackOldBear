using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public MapManager mm;
    public int mapIconNum;
    public Vector2 position;
    public Sprite revealIcon, sealedIcon;
    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        mm.SceneMovement(mapIconNum, position);
    }
}
