using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingKnife : MonoBehaviour
{
    private Vector3 ClickDaggerPosition;
    private Vector3 pos;
    private bool kill = false;
    public GameObject Knife;
    public GameObject Bear;
    public GameObject Wolf;
    public Text money;
    GameObject gameManagerObject;
    bool notEnoughMoney = false;
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        
        money.text = "10";
        if (gameManagerObject.GetComponent<ItemManager>().currentMoney() < 10)
        {
            money.color = Color.red;
            notEnoughMoney = true;
        }   
    }

    private void OnMouseDown()
    {
        if(notEnoughMoney == false)
        {
            kill = true;
            gameManagerObject.GetComponent<ItemManager>().moneyChange(-10);
        }
            
    }
    void Update()
    {

        if (kill)
        {
            transform.position = Wolf.transform.position;
            GameObject.Find("Wolf").GetComponent<MovingAnimal>().speed = 1;
        }

        
    }
}
