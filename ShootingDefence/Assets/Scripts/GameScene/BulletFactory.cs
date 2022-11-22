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
            bulletObject.gameObject.SetActive(false);
            bulletObjectPool.Enqueue(bulletObject);
        }
    }

    public Bullet DeQueueBullet()
    {
        Bullet newBullet;

        if (bulletObjectPool.Count == 0)
        {
            GameObject newObject = Instantiate(bulletGameObject, transform);
            newBullet = newObject.GetComponent<Bullet>();
        }
        else
        { 
            newBullet = bulletObjectPool.Dequeue();
            newBullet.gameObject.SetActive(true);
        }

        return newBullet;
    }

    public Bullet CreateBullet(in BulletInfo bInfo, in Vector3 bulletPos, in Quaternion bulletRot)
    {
        Bullet newBullet = DeQueueBullet();
        if (newBullet == null)
            return null;
        newBullet.gameObject.transform.position = bulletPos;
        newBullet.gameObject.transform.rotation = bulletRot;
        newBullet.RegisterBulletInfo(bInfo);
        return newBullet;
    }
}
