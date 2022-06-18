using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    public int accessoryNum, rarity, type, where;
    public Sprite thumb;
    public string accessoryName, description, additionalText;

    Accessory_Description accDesc;

    private void Start()
    {
        accDesc = GameObject.Find("Accessory_Description").GetComponent<Accessory_Description>();
    }

    private void OnMouseEnter()
    {
        accDesc.AccesorryDescON(this);
    }

    private void OnMouseExit()
    {
        accDesc.AccesorryDescOFF();
    }
}
