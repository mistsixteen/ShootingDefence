using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct GunItem
{
    public string gunName;

    public int gunMagazine;
    public int bulletLeft;
    public int totalBulletLeft;
 
    public float reloadTime;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletPushPower;

    public float fireDelay;
    public float fireAngle;
}

public class GunInventory
{
    List<GunItem> gunItemList;
    int currentIdx;

    static GunInventory instance;

    GunInventory()
    {
        gunItemList = new List<GunItem>();
        //TODO : 코드 정리해서 Factory class로 분리
        GunItem handGun = new GunItem();
        handGun.gunName = "handGun";
        handGun.gunMagazine = 12;
        handGun.bulletLeft = 12;
        handGun.totalBulletLeft = 200;
        handGun.reloadTime = 2.0f;
        handGun.bulletSpeed = 0.5f;
        handGun.bulletDamage = 5.0f;
        handGun.bulletPushPower = 2.0f;
        handGun.fireDelay = 1.0f;
        handGun.fireAngle = 0.0f;

        GunItem machineGun = new GunItem();
        machineGun.gunName = "SMG";
        machineGun.gunMagazine = 80;
        machineGun.bulletLeft = 80;
        machineGun.totalBulletLeft = 800;
        machineGun.reloadTime = 5.0f;
        machineGun.bulletSpeed = 0.5f;
        machineGun.bulletDamage = 5.0f;
        machineGun.bulletPushPower = 2.0f;
        machineGun.fireDelay = 0.1f;
        machineGun.fireAngle = 5.0f;

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
