using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
    public GameObject bear;
    Vector3 position;

    private void Start()
    {
        position = bear.transform.position;
    }

    public Vector3 getBearPosition()
    {
        return position;
    }
}
