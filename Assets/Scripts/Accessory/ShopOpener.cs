using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    public ShopManager sm;
    private void OnMouseDown()
    {
        sm.ShopOpen();
    }
}
