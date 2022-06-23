using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBox : MonoBehaviour
{
    public int type, num;
    public ShopManager shopManager;

    private void OnMouseDown()
    {
        shopManager.Buy(type, num);
    }
}
