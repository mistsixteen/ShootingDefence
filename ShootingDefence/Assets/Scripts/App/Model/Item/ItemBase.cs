using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
    public TableItemRow ItemRow;
    public ItemType Type;
    public int Tid;
    public int Amount;

    //TODO : ItemTable
    public ItemBase(ItemType type = ItemType.ItemTypeNull, TableItemRow ItemRow = null, int tid = 0, int amount = 0)
    {
        Type = type;
        Tid = tid;
        Amount = amount;
    }

}
