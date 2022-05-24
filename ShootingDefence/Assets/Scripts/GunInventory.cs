using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GunInventory
{
    List<GunItem> gunItemList;
    int currentIdx;

    static GunInventory instance;

    GunInventory()
    {
        gunItemList = new List<GunItem>();
        //TODO : �ڵ� �����ؼ� Factory class�� �и�
        GunItem handGun = new GunItem();
        handGun.gunName = "handGun";
        handGun.gunMagazine = 12;
        handGun.bulletLeft = 12;
        handGun.totalBulletLeft = 200;
        handGun.reloadTime = 2.0f;
        handGun.fireDelay = 1.0f;
        handGun.fireAngle = 0.0f;

        handGun.bInfo.bulletSpeed = 0.5f;
        handGun.bInfo.bulletDamage = 5.0f;
        handGun.bInfo.bulletPushpower = 2.0f;
        handGun.bInfo.bulletLifespan = 3000;
        handGun.bInfo.bulletColor = Color.blue;
        handGun.bInfo.trailColor = Color.blue;


        GunItem machineGun = new GunItem();
        machineGun.gunName = "SMG";
        machineGun.gunMagazine = 80;
        machineGun.bulletLeft = 80;
        machineGun.totalBulletLeft = 800;
        machineGun.reloadTime = 5.0f;
        machineGun.fireDelay = 0.1f;
        machineGun.fireAngle = 5.0f;

        machineGun.bInfo.bulletSpeed = 0.5f;
        machineGun.bInfo.bulletDamage = 5.0f;
        machineGun.bInfo.bulletPushpower = 2.0f;
        machineGun.bInfo.bulletLifespan = 3000;
        machineGun.bInfo.bulletColor = Color.blue;
        machineGun.bInfo.trailColor = Color.blue;


        gunItemList.Add(handGun);
        gunItemList.Add(machineGun);

        currentIdx = 0;
    }
    public static GunInventory GetInstance()
    {
        if(instance == null)
        {
            instance = new GunInventory();
        }
        return instance;
    }
    
    public GunItem GetCurrentItem()
    {
        return gunItemList[currentIdx];
    }

    public void SetGunIdx(int idx)
    {
        if (idx >= gunItemList.Count)
            currentIdx = 0;
        else
            currentIdx = idx;
    }

}
