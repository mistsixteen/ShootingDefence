using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun
{
    public string gunName;

    public int gunMagazine;
    public int bulletLeft;
    public int totalBulletLeft;

    public float fireDelay;
    public float fireAngle;

    public float reloadTime;

    public BulletInfo bInfo;

    public bool IsAttackAble()
    {
        if(bulletLeft > 0)
            return true;
        return false;
    }


}