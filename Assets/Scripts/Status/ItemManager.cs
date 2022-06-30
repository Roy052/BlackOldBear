using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    const int moneyMAX = 9999;
    const int itemMAX = 99;
    public int money;
    public int[] items;
    public UIBarManager uIBarManager;
    private void Start()
    {
        items = new int[2];
        for (int i = 0; i < 2; i++) items[i] = 0;
    }

    public int currentMoney()
    {
        return money;
    }

    public int[] currentItem()
    {
        return items;
    }

    public int moneyChange(int num)
    {
        if (num < 0 && money + num < 0) return -1;
        else if (num > 0 && money + num > moneyMAX) money = moneyMAX;
        else money += num;

        if(uIBarManager != null)
            uIBarManager.UITextUpdate(1, money, 0);
        return 1;
    }

    public int itemChange(int type, int num)
    {
        if (num < 0 && items[type] + num < 0) return -1;
        else if (items[type] + num > itemMAX) items[type] = itemMAX;
        else items[type] += num;

        if (uIBarManager != null)
            uIBarManager.UITextUpdate(type+2, items[type], 0);
        return 1;
    }
}
