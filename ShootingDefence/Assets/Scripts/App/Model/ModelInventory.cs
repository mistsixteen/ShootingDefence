using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInventory
{
    private List<ItemBase> InvenList;

    public ModelInventory()
    {
        for(int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
        {
            InvenList.Add(null);
        }
    }

    public bool AddItem(int tid, int count)
    {
        return true;
    }


    //Debug Functions
    public void Debug_GetInventoryList()
    {
        for (int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
        {
            if (InvenList[i] == null)
                Debug.Log("Item Slot " + i + " : NULL");
            else
                Debug.Log("Item Slot " + i + " : " + InvenList[i].ItemTid);
        }
    }
}
