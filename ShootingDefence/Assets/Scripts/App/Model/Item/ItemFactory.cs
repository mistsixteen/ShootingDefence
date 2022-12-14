using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    public static ItemBase CreateEmpty()
    {
        return new ItemBase();
    }

    public static ItemBase CreateItem(int itemTID)
    {
        var tableItemRow = AppInstance.GetInstance().TableManager.TableItem.GetTableRow(itemTID);
        if (tableItemRow == null)
            return null;

        switch (tableItemRow.Type)
        {
            case "Weapon":
                var tableWeaponRow = AppInstance.GetInstance().TableManager.TableWeapon.GetTableRow(itemTID);
                if (tableWeaponRow == null)
                    return null;
                ItemWeapon itemWeapon = new ItemWeapon(tableItemRow, tableWeaponRow);
                return itemWeapon as ItemBase;
            default:
                return null;
        }

        return null;
    }
}
