using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageButton : MonoBehaviour
{
    MapManager mapManager;

    private void Start()
    {
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }
    private void OnMouseDown()
    {
        mapManager.NextStage();
    }
}
