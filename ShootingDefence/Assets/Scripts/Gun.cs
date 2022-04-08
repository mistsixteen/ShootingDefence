using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletSpawn;
    public float delay_fire = 0.1f;
    private float timeStamp = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if ((Time.time >= timeStamp))
            {
                Fire();
                timeStamp = Time.time + delay_fire;
            }
        }
        else
        {
        }
    }
    void Fire()
    {
        var bullet = (GameObject)Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
