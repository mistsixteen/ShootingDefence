using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuickBar : MonoBehaviour
{
    private UIQuickSlot[] quickSlots = null;

    private void OnEnable()
    {
        if(quickSlots == null)
            quickSlots = GetComponentsInChildren<UIQuickSlot>();
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        if (quickSlots == null)
            return;

        var ModelInventory = AppInstance.GetInstance().ModelManager.ModelInventory;

        for (int i = 0; i < quickSlots.Length; i++)
        {
            quickSlots[i]?.SetSelected(ModelInventory.currentIdx == i);
            quickSlots[i]?.SetSlotText(ModelInventory.GetItemName(i));
        }
    }
}
