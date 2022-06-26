using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornfireBox : MonoBehaviour
{
    public int type, num;
    public BornfireManager bornfireManager;

    private void OnMouseDown()
    {
        if (type == 0) bornfireManager.Heal();
        else if (type == 1 && num == 0) bornfireManager.Upgrade(0);
        else bornfireManager.Upgrade(1);
    }
}
