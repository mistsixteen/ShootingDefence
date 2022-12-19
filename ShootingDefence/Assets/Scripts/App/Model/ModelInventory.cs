using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelInventory
{
    private List<ItemBase> invenList;
    private ItemBase currentEquipItem;

    public UnityAction onChanged;
    public int currentIdx;

    public ModelInventory()
    {
        invenList = new List<ItemBase>();
        for (int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
        {
            invenList.Add(new ItemBase());
        }
        //starting Item;
        currentIdx = -1;
        AddItem(1);
        AddItem(2);
        currentEquipItem = null;
    }

    public void SetGunIdx(int idx)
    {
        currentIdx = idx;
        if (invenList[idx].Type == ItemType.ItemTypeNull)
        {
            currentEquipItem = null;
        }
        else
            currentEquipItem = invenList[idx];

        onChanged.Invoke();
    }

    public bool IsCurrentItemWeapon()
    {
        if (currentEquipItem is ItemWeapon)
            return true;
        return false;
    }
    public ItemWeapon GetCurrentItemWeapon()
    {
        var currentWeapon = (currentEquipItem as ItemWeapon);
        return currentWeapon;
    }

    public bool IsAttackAble()
    {
        if(currentEquipItem is ItemWeapon)
        {
            return (currentEquipItem as ItemWeapon).IsAttackAble();
        }
        return false;
    }

    public void UseItem()
    {
        if (currentEquipItem is ItemWeapon)
        {
            (currentEquipItem as ItemWeapon).DecreaseMag();
        }
        onChanged.Invoke();
    }

    public bool IsReloadAble()
    {
        //TODO : Bullet check
        if(IsCurrentItemWeapon())
            return true;
        return false;
    }
    public void ReloadWeapon()
    {
        if (currentEquipItem is ItemWeapon)
        {
            (currentEquipItem as ItemWeapon).ReloadMag(40);
            onChanged.Invoke();
        }
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
