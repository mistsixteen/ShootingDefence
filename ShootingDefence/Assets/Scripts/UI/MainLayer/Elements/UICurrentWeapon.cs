using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurrentWeapon : MonoBehaviour
{
    [SerializeField]
    private Text textWeaponName;
    [SerializeField]
    private Text textCurrentBullet;

    public void UpdateStatus()
    {
        var ModelInventory = AppInstance.GetInstance().ModelManager.ModelInventory;

        if (ModelInventory.IsCurrentItemWeapon())
        {
            var weapon = ModelInventory.GetCurrentItemWeapon();
            textWeaponName.text = "Weapon : " + weapon.ItemRow.Name;
            textCurrentBullet.text = weapon.CurrentMag.ToString();
        }
        else
        {
            textWeaponName.text = "No Weapon";
            textCurrentBullet.text = " ";
        }
    }

    public void SetReloadMode()
    {
        textWeaponName.text = "Reloading";
        textCurrentBullet.text = " ";
    }
}
