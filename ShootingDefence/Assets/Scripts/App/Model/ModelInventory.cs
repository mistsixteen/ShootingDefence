using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelInventory
{
    private List<ItemBase> invenList;
    private ItemBase currentEquipItem;

    public int currentIdx;

    public ModelInventory()
    {
        InitializeModelInventory();
    }

    public void InitializeModelInventory()
    {
        invenList = new List<ItemBase>();
        for (int i = 0; i < GlobalCommonValues.InventoryMaxSize; i++)
        {
            invenList.Add(new ItemBase());
        }
        //starting Item;
        currentIdx = 0;
        currentEquipItem = null;

        AddStartingItem();
        SelectQuickbar(0);
    }

    public void AddStartingItem()
    {
        AddItem(1);
        AddItem(2);
    }

    public void SelectQuickbar(int idx)
    {
        if(idx < 0 || idx >= GlobalCommonValues.QuickBarMaxSize)
        {
            return;
        }

        currentIdx = idx;

        if (invenList[idx].Type == ItemType.ItemTypeNull)
        {
            currentEquipItem = null;
        }
        else
        {
            currentEquipItem = invenList[idx];
        }
        EventSystem.GetInstance()?.InvokeEvent(EventType.onModelInvenChanged);
    }
    public void SelectQuickbarPrev()
    {
        SelectQuickbar(currentIdx - 1);
    }
    public void SelectQuickbarNext()
    {
        SelectQuickbar(currentIdx + 1);
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
        EventSystem.GetInstance()?.InvokeEvent(EventType.onModelInvenChanged);
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
        if (IsReloadAble())
        {
            (currentEquipItem as ItemWeapon).ReloadMag(40);
            EventSystem.GetInstance()?.InvokeEvent(EventType.onModelInvenChanged);
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
