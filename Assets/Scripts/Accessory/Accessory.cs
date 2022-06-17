using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    public int accessoryNum, rarity, type;
    public Sprite thumb;
    public string accessoryName, description, additionalText;

    public Accessory_Description accDesc;


    private void OnMouseEnter()
    {
        accDesc.AccesorryDescON(this);
    }

    private void OnMouseExit()
    {
        accDesc.AccesorryDescOFF();
    }
}
