using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GunController : MonoBehaviour
{
    private ModelInventory modelInventory;
    private ItemWeapon currentWeapon;
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
        //myInventory = GunInventory.GetInstance();
        //CurrentWeapon = myInventory.GetCurrentItem();
        modelInventory = AppInstance.GetInstance().ModelManager.ModelInventory;
        StartCoroutine(GunRoutine());
    }

    IEnumerator GunRoutine()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                if (modelInventory.IsCurrentItemWeapon() && (Time.time >= timeStamp))
                {
                    Fire();
                    //timeStamp = Time.time + CurrentWeapon.fireDelay;
                }
            }
            if (Input.GetButton("RWeapon"))
            {
                modelInventory.SetGunIdx(1);
            }
            else if (Input.GetButton("LWeapon"))
            {
                modelInventory.SetGunIdx(0);
            }
            else if (Input.GetButton("Reload"))
            {
                if(modelInventory.IsReloadAble())
                {
                    myLineRenderer.enabled = false;
                    //myUI.isReload = true;
                    delay_reload = 2.0f;
                    cGunState = gunState.gunStateReloading;
                    for (int i = 0; i < 100; i++)
                    {
                        yield return new WaitForSeconds(delay_reload / 100);
                        delay_reload -= delay_reload / 100;

                    }
                    delay_reload = 0.0f;

                    myLineRenderer.enabled = true;
                    //myUI.isReload = false;
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
        if (modelInventory.GetCurrentItem() is ItemWeapon)
        {
            var curWeapon = modelInventory.GetCurrentItem() as ItemWeapon;
            if (curWeapon.IsAttackAble())
            {
                var newBullet = BulletFactory.GetInstance().CreateBullet(curWeapon.ProjRow, BulletSpawn.position, BulletSpawn.rotation);
                newBullet.bulletFaction = ObjectFaction.Ally;
                newBullet.transform.Rotate(new Vector3(0.0f, Random.Range(0 - tileAngle, tileAngle), 0.0f));
                curWeapon.DecreaseMag();
            }
        }
    }
}
