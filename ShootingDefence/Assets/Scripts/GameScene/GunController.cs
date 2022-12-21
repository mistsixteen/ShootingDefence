using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GunController : MonoBehaviour
{
    private ModelInventory modelInventory;
    public Transform BulletSpawn;
    public LineRenderer myLineRenderer;

    public float delay_fire = 0.01f;
    public float delay_reload = 0.0f;
    private float timeStamp = 0.0f;
    private float tileAngle = 10.0f;
    public bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        modelInventory = AppInstance.GetInstance().ModelManager.ModelInventory;    
    }

    private void Update()
    {
        if (isReloading)
        {
            myLineRenderer.enabled = false;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (modelInventory.IsCurrentItemWeapon() && (Time.time >= timeStamp))
            {
                Fire();
                timeStamp = Time.time + 0.25f;
            }
        }
        if (Input.GetButtonUp("RWeapon"))
        {
            modelInventory.SelectQuickbarNext();
        }
        else if (Input.GetButtonUp("LWeapon"))
        {
            modelInventory.SelectQuickbarPrev();
        }
        else if (Input.GetButtonUp("Reload"))
        {
            if (modelInventory.IsReloadAble())
            {
                StartCoroutine(ReloadCoroutine(2.0f));
            }
        }

        //조준선 : LineRenderer 사용
        //무기가 없거나 in Reload인 경우 해제
        if (modelInventory.IsCurrentItemWeapon())
        {
            myLineRenderer.enabled = true;
            myLineRenderer.SetPosition(0, BulletSpawn.position);
            Vector3 secondPos = BulletSpawn.forward;
            secondPos.Normalize();
            myLineRenderer.SetPosition(1, BulletSpawn.position + secondPos * 2000.0f);
        }
        else
            myLineRenderer.enabled = false;

    }

    IEnumerator ReloadCoroutine(float time)
    {
        isReloading = true;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(time / 100.0f);
        }
        modelInventory.ReloadWeapon();
        isReloading = false;
    }

    void Fire()
    {
        if (modelInventory.GetCurrentItem() is ItemWeapon)
        {
            if (modelInventory.IsAttackAble())
            {
                var curWeapon = modelInventory.GetCurrentItem() as ItemWeapon;
                var newBullet = BulletFactory.GetInstance().CreateBullet(curWeapon.ProjRow, BulletSpawn.position, BulletSpawn.rotation);
                newBullet.bulletFaction = ObjectFaction.Ally;
                newBullet.transform.Rotate(new Vector3(0.0f, Random.Range(0 - tileAngle, tileAngle), 0.0f));
                modelInventory.UseItem();
            }
        }
    }
}
