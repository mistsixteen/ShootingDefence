using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GunController : MonoBehaviour
{
    private GameObject Bullet;
    public GunItem currentGunItem;

    public Transform BulletSpawn;
    public float delay_fire = 0.01f;
    private float timeStamp = 0.0f;
    private float tileAngle = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        currentGunItem = GunInventory.GetInstance().GetCurrentItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if ((Time.time >= timeStamp))
            {
                Fire();
                timeStamp = Time.time + currentGunItem.fireDelay;
            }
        }
        else
        {

        }
    }
    void Fire()
    {
        GameObject newBullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        newBullet.GetComponent<Bullet>().registerBulletInfo(currentGunItem);
        newBullet.transform.Rotate(new Vector3(0.0f, Random.Range(0 - tileAngle, tileAngle), 0.0f));
    }
}
