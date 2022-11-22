using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{

    private static BulletFactory instance;
    private Queue<Bullet> bulletObjectPool;
    private GameObject bulletGameObject = null;

    public static BulletFactory GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        bulletGameObject = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletObjectPool = new Queue<Bullet>();
    }

    public void EnQueueBullet(Bullet bulletObject)
    {
        if (bulletObject){
            bulletObjectPool.Enqueue(bulletObject);
        }
    }

    public GameObject CreateBullet(in BulletInfo bInfo, in Vector3 bulletPos, in Quaternion bulletRot)
    {
        GameObject newObject = Instantiate(bulletGameObject, bulletPos, bulletRot, transform);
        if(newObject == null)
        {
            return null;
        }
        newObject.GetComponent<Bullet>().RegisterBulletInfo(bInfo);
        return newObject;
    }


}
