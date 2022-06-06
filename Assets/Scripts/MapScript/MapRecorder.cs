using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRecorder : MonoBehaviour
{
    public Vector2 currentPosition;
    public bool recorded;
    int[,] map;
    bool[,] visitableMap;

    private void Start()
    {
        recorded = false;
    }


    public void RecordMap(int[,] map)
    {
        this.map = map.Clone() as int[,];
        recorded = true;
    }

    public void RecordVisitableMap(bool[,] visitableMap)
    {
        this.visitableMap = visitableMap.Clone() as bool[,];
    }

    public int[,] ReturnMap()
    {
        return map;
    }

    public bool[,] ReturnVisitableMap()
    {
        return visitableMap;
    }
}
