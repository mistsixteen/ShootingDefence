using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager
{
    // Start is called before the first frame update

    public TableItem TableItem;
    public TableWeapon TableWeapon;
    public TableProjectile TableProjectile;

    public TableManager()
    {
        TableItem = new TableItem();
        TableWeapon = new TableWeapon();
        TableProjectile = new TableProjectile();
    }
}
