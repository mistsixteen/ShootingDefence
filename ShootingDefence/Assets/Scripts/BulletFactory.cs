using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory
{
    private static BulletFactory instance;

    GameObject bulletGameObject = null;

    public static BulletFactory GetInstance()
    {
        if(instance == null)
        {
            instance = new BulletFactory();
        }
        return instance;
    }

    public BulletFactory()
    {
        bulletGameObject = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    public GameObject createBullet(in BulletInfo bInfo, in Vector3 bulletPos, in Quaternion bulletRot)
    {
        GameObject newObject = GameObject.Instantiate(bulletGameObject, bulletPos, bulletRot);
        if(newObject == null)
        {
            Debug.Log("NULL EXEP");
        }
        newObject.GetComponent<Bullet>().RegisterBulletInfo(bInfo);

        return newObject;
    }


}
