using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase
{
    public int ItemTid;
    public int ItemAmount;
    public int ItemType;

    //TODO : ItemTable

    public ItemBase()
    {
        ItemTid = 0;
        ItemAmount = 0;
        ItemType = 0;
    }

}
