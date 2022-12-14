using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunInventory myInventory;

    public Transform BulletSpawn;
    public LineRenderer myLineRenderer;
    public PlayerUI myUI;

    public gunState cGunState = gunState.gunStateIdle;

    public float delay_fire = 0.01f;
    public float delay_reload = 0.0f;
    private float timeStamp = 0.0f;
    private float tileAngle = 10.0f;

    WeaponGun CurrentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        myInventory = GunInventory.GetInstance();
        CurrentWeapon = myInventory.GetCurrentItem();
        StartCoroutine(GunRoutine());

    }

    IEnumerator GunRoutine()
    {
        while(true)
        { 
            if (Input.GetMouseButton(0))
            {
                if (CurrentWeapon.IsAttackAble() && (Time.time >= timeStamp))
                {
                    Fire();
                    timeStamp = Time.time + CurrentWeapon.fireDelay;
                }
            }
            if (Input.GetButton("RWeapon"))
            {
                GunInventory.GetInstance().SetGunIdx(1);
                CurrentWeapon = myInventory.GetCurrentItem();
                myUI.SelectItem(1);
            }
            else if (Input.GetButton("LWeapon"))
            {
                GunInventory.GetInstance().SetGunIdx(0);
                CurrentWeapon = myInventory.GetCurrentItem();
                myUI.SelectItem(0);
            }
            else if (Input.GetButton("Reload"))
            {
                if(CurrentWeapon.totalBulletLeft > 0 && CurrentWeapon.bulletLeft != CurrentWeapon.gunMagazine)
                {
                    myLineRenderer.enabled = false;
                    myUI.isReload = true;
                    delay_reload = CurrentWeapon.reloadTime;
                    cGunState = gunState.gunStateReloading;
                    for(int i = 0; i < 50; i++)
                    {
                        yield return new WaitForSeconds(CurrentWeapon.reloadTime / 50);
                        delay_reload -= CurrentWeapon.reloadTime / 50;
                        
                    }
                    delay_reload = 0.0f;

                    if (CurrentWeapon.totalBulletLeft >= CurrentWeapon.gunMagazine)
                    {
                        CurrentWeapon.bulletLeft = CurrentWeapon.gunMagazine;
                        CurrentWeapon.totalBulletLeft -= CurrentWeapon.gunMagazine; 
                    }
                    else
                    {
                        CurrentWeapon.bulletLeft = CurrentWeapon.totalBulletLeft;
                        CurrentWeapon.totalBulletLeft = 0;
                    }

                    myLineRenderer.enabled = true;
                    myUI.isReload = false;
                    cGunState = gunState.gunStateIdle;
                }

            }
            //조준선 : LineRenderer 사용
            myLineRenderer.SetPosition(0, BulletSpawn.position);
            Vector3 secondPos = BulletSpawn.forward;
            secondPos.Normalize();
            myLineRenderer.SetPosition(1, BulletSpawn.position + secondPos * 2000.0f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Fire()
    {
        var newBullet = BulletFactory.GetInstance().CreateBullet(CurrentWeapon.bInfo, BulletSpawn.position, BulletSpawn.rotation);
        newBullet.bulletFaction = ObjectFaction.Ally;
        newBullet.transform.Rotate(new Vector3(0.0f, Random.Range(0 - tileAngle, tileAngle), 0.0f));
        CurrentWeapon.bulletLeft--;
    }
}
