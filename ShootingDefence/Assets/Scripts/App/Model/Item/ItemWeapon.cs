using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : ItemBase
{
    //public int ItemTid;
    //public int ItemAmount;

    public TableWeaponRow WeaponRow;
    public TableProjectileRow ProjRow;

    public int CurrentMag;
    public int BulletTid;

    public ItemWeapon(TableItemRow _itemRow, TableWeaponRow _WeaponRow)
    {
        Type = ItemType.ItemTypeWeapon;
        ItemRow = _itemRow;
        WeaponRow = _WeaponRow;
        ProjRow = AppInstance.GetInstance().TableManager.TableProjectile.GetTableRow(WeaponRow.ProjTID);
        CurrentMag = WeaponRow.MagazineSize;
    }

    public bool IsAttackAble()
    {
        if (CurrentMag > 0)
            return true;
        return false;
    }

    public void DecreaseMag()
    {
        if (CurrentMag > 0)
            CurrentMag--;
    }

    public bool isReloadAble()
    {
        return true;
    }

    public void ReloadMag(int bullet)
    {
        CurrentMag = bullet;
    }
}
