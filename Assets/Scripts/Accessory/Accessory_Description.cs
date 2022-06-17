using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accessory_Description : MonoBehaviour
{
    private Vector2 mousePosition;
    public Image accImage;
    public Text nameText, descText, addText;
    public Canvas canvas;

    public bool ON = false;

    private void Start()
    {
        AccesorryDescOFF();
    }
    private void Update()
    {
        if (ON)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x + 2.2f, mousePosition.y - 1.5f);
            accImage.transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y + 0.7f);
            nameText.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.7f);
            descText.transform.position = new Vector2(transform.position.x, transform.position.y - 0.4f);
            addText.transform.position = new Vector2(transform.position.x, transform.position.y - 0.9f);
        }
        else
        {
            transform.position = new Vector2(-10, -10);
            accImage.transform.position = new Vector2(-10, -10);
            nameText.transform.position = new Vector2(-10, -10);
            descText.transform.position = new Vector2(-10, -10);
            addText.transform.position = new Vector2(-10, -10);
        }
        if (canvas.worldCamera == null)
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    public void AccesorryDescON(Accessory accessory)
    {
        accImage.sprite = accessory.thumb;
        nameText.text = accessory.accessoryName;
        switch (accessory.rarity)
        {
            case 0:
                nameText.color = Color.white;
                break;
            case 1:
                nameText.color = Color.green;
                break;
            case 2:
                nameText.color = Color.blue;
                break;
            case 3:
                nameText.color = Color.magenta;
                break;

        }
        descText.text = accessory.description;
        addText.text = accessory.additionalText;
        ON = true;
    }

    public void AccesorryDescOFF()
    {
        ON = false;
    }
}
