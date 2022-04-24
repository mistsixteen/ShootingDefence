using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gunState
{
    gunStateEmpty,
    gunStateIdle,
    gunStateReloading
}


public class GunController : MonoBehaviour
{
    private GameObject Bullet;
    public GunItem currentGunItem;

    public Transform BulletSpawn;
    public LineRenderer myLineRenderer;

    public gunState cGunState = gunState.gunStateIdle;

    public float delay_fire = 0.01f;
    public float delay_reload = 0.0f;
    private float timeStamp = 0.0f;
    private float tileAngle = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        currentGunItem = GunInventory.GetInstance().GetCurrentItem();
        StartCoroutine(GunRoutine());

    }

    bool CanFire()
    {
        if (currentGunItem.bulletLeft > 0)
            return true;
        return false;
    }
    IEnumerator GunRoutine()
    {
        while(true)
        { 
            if (Input.GetMouseButton(0))
            {
                if (CanFire() && (Time.time >= timeStamp))
                {
                    Fire();
                    timeStamp = Time.time + currentGunItem.fireDelay;
                }
            }
            if (Input.GetButton("RWeapon"))
            {
                GunInventory.GetInstance().SetGunIdx(1);
                currentGunItem = GunInventory.GetInstance().GetCurrentItem();
            }
            else if (Input.GetButton("LWeapon"))
            {
                GunInventory.GetInstance().SetGunIdx(0);
                currentGunItem = GunInventory.GetInstance().GetCurrentItem();
            }
            else if (Input.GetButton("Reload"))
            {
                if(currentGunItem.totalBulletLeft > 0 && currentGunItem.bulletLeft != currentGunItem.gunMagazine)
                {
                    myLineRenderer.enabled = false;
                    delay_reload = currentGunItem.reloadTime;
                    cGunState = gunState.gunStateReloading;
                    for(int i = 0; i < 50; i++)
                    {
                        yield return new WaitForSeconds(currentGunItem.reloadTime / 50);
                        delay_reload -= currentGunItem.reloadTime / 50;
                        
                    }
                    delay_reload = 0.0f;

                    if (currentGunItem.totalBulletLeft >= currentGunItem.gunMagazine)
                    {
                        currentGunItem.bulletLeft = currentGunItem.gunMagazine;
                        currentGunItem.totalBulletLeft -= currentGunItem.gunMagazine; 
                    }
                    else
                    {
                        currentGunItem.bulletLeft = currentGunItem.totalBulletLeft;
                        currentGunItem.totalBulletLeft = 0;
                    }

                    myLineRenderer.enabled = true;
                    cGunState = gunState.gunStateIdle;
                }

            }
            //조준선 : LineRenderer 사용
            myLineRenderer.SetPosition(0, BulletSpawn.position);
            myLineRenderer.SetPosition(1, BulletSpawn.position + BulletSpawn.forward * 40.0f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Fire()
    {
        GameObject newBullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        newBullet.GetComponent<Bullet>().registerBulletInfo(currentGunItem);
        newBullet.transform.Rotate(new Vector3(0.0f, Random.Range(0 - tileAngle, tileAngle), 0.0f));
        currentGunItem.bulletLeft--;
    }
}
