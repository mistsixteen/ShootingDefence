using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveVector;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletPushpower;
    public int bulletLifespan;
    private Coroutine bulletCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        moveVector = transform.forward;
        bulletCoroutine = StartCoroutine(BulletRoutine());
    }

    public void registerBulletInfo(in GunItem gunInfo )
    {
        bulletSpeed = gunInfo.bulletSpeed;
        bulletDamage = gunInfo.bulletDamage;
        bulletPushpower = gunInfo.bulletPushPower;

    }

    IEnumerator BulletRoutine()
    {
        while(bulletLifespan >= 0)
        {
            bulletLifespan--;
            transform.position += moveVector * bulletSpeed;

            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        Vector3 direction = moveVector.normalized;
        direction.y = 0;

        if (collision.gameObject.TryGetComponent(out Damageable dObject))
        {
            dObject.GetDamage(bulletDamage, bulletPushpower, direction);
        }
        
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }


}
