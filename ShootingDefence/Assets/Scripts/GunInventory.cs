using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunItem
{
    public string gunName;

    public int currentMagazine;
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
        GunItem handgun = new GunItem();
        handgun.gunName = "HandGun";
        handgun.currentMagazine = 12;
        handgun.bulletLeft = 12;
        handgun.totalBulletLeft = 200;
        handgun.reloadTime = 5.0f;
        handgun.bulletSpeed = 0.5f;
        handgun.bulletDamage = 5.0f;
        handgun.bulletPushPower = 2.0f;
        handgun.fireDelay = 1.0f;
        handgun.fireAngle = 0.0f;

        gunItemList.Add(handgun);

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
