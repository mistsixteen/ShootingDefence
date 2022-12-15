using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInventory
{
    private List<ItemBase> invenList;
    private ItemBase currentEquipItem;

    public ModelInventory()
    {
        invenList = new List<ItemBase>();
        for (int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
        {
            invenList.Add(new ItemBase());
        }
        //starting Item;
        AddItem(1);
        AddItem(2);
        currentEquipItem = null;

    }

    public void SetGunIdx(int idx)
    {
        if (invenList[idx].Type == ItemType.ItemTypeNull)
        {
            currentEquipItem = null;
        }
        else
            currentEquipItem = invenList[idx];
    }

    public bool IsCurrentItemWeapon()
    {
        if (currentEquipItem is ItemWeapon)
            return true;
        return false;
    }

    public bool IsReloadAble()
    {
        //TODO : Bullet check
        if(IsCurrentItemWeapon())
            return true;
        return false;
    }
    public void Reload()
    {

    }

    public ItemBase GetCurrentItem()
    {
        return currentEquipItem;
    }

    public bool AddItem(int itemTID)
    {
        ItemBase newItem = CreateNewItem(itemTID);
        if (newItem != null)
        {
            for (int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
            {
                if (invenList[i].Type == ItemType.ItemTypeNull)
                {
                    Debug.Log("Add Item on Slot" + i);
                    invenList[i] = newItem;
                    return true;
                }
            }
        }

        return false;
    }

    public ItemBase CreateNewItem(int itemTid)
    {
        TableItemRow tableRow = AppInstance.GetInstance().TableManager.TableItem.GetTableRow(itemTid);
        if(tableRow != null)
        {
            switch (tableRow.Type)
            {
                case "Weapon":
                    var tableWeaponRow = AppInstance.GetInstance().TableManager.TableWeapon.GetTableRow(itemTid);
                    if (tableWeaponRow == null) 
                        return null;
                    var newWeapon = new ItemWeapon(tableRow, tableWeaponRow);
                    return newWeapon as ItemBase;

                default:
                    return null;
            }
        }
        else
        {
            return null;
        }

    }


    //Debug Functions
    public void Debug_GetInventoryList()
    {
        for (int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
        {
            if (invenList[i] == null)
                Debug.Log("Item Slot " + i + " : NULL");
            else
                Debug.Log("Item Slot " + i + " : " + invenList[i].Tid);
        }
    }
}
