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
    public ObjectFaction bulletFaction;

    //Coroutines
    private Coroutine bulletCoroutine;

    //Components
    private Renderer myRenderer;
    private TrailRenderer myTrailRenderer;

    private Color myColor, trailStartColor, trailEndColor;



    // Start is called before the first frame update
    void Start()
    {
        moveVector = transform.forward;
        bulletCoroutine = StartCoroutine(BulletRoutine());

        myRenderer = GetComponent<Renderer>();
        myRenderer.material.color = myColor;
        myTrailRenderer = GetComponent<TrailRenderer>();
        myTrailRenderer.startColor = trailStartColor;
        myTrailRenderer.endColor = trailEndColor;
    }

    public void registerBulletInfo(in GunItem gunInfo )
    {
        bulletSpeed = gunInfo.bulletSpeed;
        bulletDamage = gunInfo.bulletDamage;
        bulletPushpower = gunInfo.bulletPushPower;
    }

    public void RegisterBulletInfo(in BulletInfo bInfo)
    {
        bulletSpeed = bInfo.bulletSpeed;
        bulletDamage = bInfo.bulletDamage;
        bulletPushpower = bInfo.bulletPushpower;
        bulletLifespan = bInfo.bulletLifespan;
        //bulletColor
        if(myRenderer != null)
            myRenderer.material.color = bInfo.bulletColor;
        myColor = bInfo.bulletColor;
        //trailColor
        if(myTrailRenderer != null)
        {
            myTrailRenderer.startColor = bInfo.trailColor;
            myTrailRenderer.endColor = bInfo.trailColor;
        }
        trailStartColor = bInfo.trailColor;
        trailEndColor = bInfo.trailColor;
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
            if (dObject.getFaction() != bulletFaction)
            {
                dObject.GetDamage(bulletDamage, bulletPushpower, direction);
                Destroy(this.gameObject);
            }
            else // bypass Friendly Object
            {

            }
                
        }
        else
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

}
