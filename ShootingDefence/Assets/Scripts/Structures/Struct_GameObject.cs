using UnityEngine;

public struct BulletInfo
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletPushpower;
    public int bulletLifespan;
    public Color bulletColor;
    public Color trailColor;
}

public struct GunItem
{
    public string gunName;

    public int gunMagazine;
    public int bulletLeft;
    public int totalBulletLeft;

    public float fireDelay;
    public float fireAngle;

    public float reloadTime;

    public BulletInfo bInfo;
}