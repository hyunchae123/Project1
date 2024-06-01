using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int Starcount;
    public int CarrotCount;

    public void Start()
    {
        Starcount = 0;
        CarrotCount = 3;
    }

    public void StarPlus()
    {
        Starcount++;
    }

    public void CarrotMinus()
    {
        if (CarrotCount == 0)
            return;

        CarrotCount--;
    }

    public void CarrotPlus()
    {
        if (CarrotCount == 3)
            return;

        CarrotCount++;
    }
}
