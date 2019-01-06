using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : Item
{
    public override ItemType GetItemType()
    {
        return ItemType.INE;
    }

    public override float GetSpeed()
    {
        return 10.0f;
    }
}
