using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public UIBarManager uIBarManager;

    int health, maxhealth;
    GameManager gm;
    
    private void Start()
    {
        gm = this.GetComponent<GameManager>();
        uIBarManager = this.GetComponent<UIBarManager>();

        maxhealth = 100;
        health = maxhealth;

        uIBarManager.UITextUpdate(0, health, maxhealth);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxhealth()
    {
        return maxhealth;
    }

    public void ChangeHealth(int num)
    {
        if (health + num <= 0) gm.GameOver();
        else if (health + num > maxhealth) health = maxhealth;
        else health += num;

        uIBarManager.UITextUpdate(0, health, maxhealth);
    }

    public void ChangeMaxhealth(int num)
    {
        if (maxhealth + num < 0) gm.GameOver();
        else
        {
            maxhealth += num;
            health += num;
        }

        uIBarManager.UITextUpdate(0, health, maxhealth);
    }
}
