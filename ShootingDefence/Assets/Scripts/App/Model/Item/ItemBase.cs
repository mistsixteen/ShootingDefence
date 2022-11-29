using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase
{
    public int ItemType;
    public int ItemTid;
    public int ItemAmount;

    //TODO : ItemTable
    public ItemBase(int type = 0, int tid = 0, int amount = 0)
    {
        ItemType = type;
        ItemTid = tid;
        ItemAmount = amount;
    }

}
