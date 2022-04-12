using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveVector;
    public float bulletSpeed;
    public int bulletLifespan;
    private Coroutine bulletCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        moveVector = transform.forward;
        bulletCoroutine = StartCoroutine(BulletRoutine());
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

        //Todo : 발사시 position, or 발사시 pos + 피격시 pos / 2로 변경
        // 현재 Side로 밀리는 현상 발생
        Debug.Log("Bullet Hit!!");
        if (collision.gameObject.TryGetComponent(out Damageable dObject))
        {
            dObject.GetDamage(1, 0.5f, direction);
        }
        
        Destroy(this.gameObject);
    }


}
