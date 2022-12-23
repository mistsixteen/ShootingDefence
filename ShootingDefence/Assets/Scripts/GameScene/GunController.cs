using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private ModelInventory modelInventory;
    [SerializeField]
    private MeshRenderer myMeshRenderer;

    public Transform BulletSpawn;

    public LineRenderer myLineRenderer;

    private float timeStamp = 0.0f;
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
                timeStamp = Time.time + modelInventory.GetCurrentItemWeapon().WeaponRow.FireTime;
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
            SetWeaponVisible(true);
            myLineRenderer.enabled = true;
            myLineRenderer.SetPosition(0, BulletSpawn.position);
            Vector3 secondPos = BulletSpawn.forward;
            secondPos.Normalize();
            myLineRenderer.SetPosition(1, BulletSpawn.position + secondPos * 2000.0f);
        }
        else
        {
            myLineRenderer.enabled = false;
            SetWeaponVisible(false);
        }

    }

    IEnumerator ReloadCoroutine(float time)
    {
        isReloading = true;
        EventSystem.GetInstance().InvokeEvent(EventType.onReloadStarted);
        yield return new WaitForSeconds(time);
        modelInventory.ReloadWeapon();
        isReloading = false;
        EventSystem.GetInstance().InvokeEvent(EventType.onReloadFinished);
    }

    void Fire()
    {
        if (modelInventory.GetCurrentItem() is ItemWeapon)
        {
            var spreadAngle = modelInventory.GetCurrentItemWeapon().WeaponRow.FireSpread / 2;
            if (modelInventory.IsAttackAble())
            {
                var curWeapon = modelInventory.GetCurrentItem() as ItemWeapon;
                var angle = Random.Range(0 - spreadAngle, spreadAngle);
                var newBullet = BulletFactory.GetInstance().CreateBullet(curWeapon.ProjRow, BulletSpawn.position, BulletSpawn.rotation, angle);
                newBullet.bulletFaction = ObjectFaction.Ally;

                modelInventory.UseItem();
            }
        }
    }

    void SetWeaponVisible(bool isVisible)
    {
        myMeshRenderer.enabled = isVisible;
    }
}
