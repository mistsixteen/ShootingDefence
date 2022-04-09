using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private GameObject Bullet;
    public Transform BulletSpawn;
    public float delay_fire = 0.05f;
    private float timeStamp = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
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
        Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
    }
}
