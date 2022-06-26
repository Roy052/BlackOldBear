using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public ChestManager chestManager;

    private void OnMouseDown()
    {
        if (chestManager.chestON == false)
            chestManager.ChestON();
    }
}
