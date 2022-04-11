using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 m_moveVector;
    public float m_Speed;
    public int m_Lifespan;
    private Coroutine m_bCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        m_moveVector = transform.forward;
        m_bCoroutine = StartCoroutine(BulletMove());
    }

    IEnumerator BulletMove()
    {
        while(m_Lifespan >= 0)
        {
            m_Lifespan--;
            transform.position += m_moveVector * m_Speed;

            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Bullet Hit!!");
        if (collision.gameObject.TryGetComponent(out Damageable dObject))
        {
            dObject.getDamage(10);
        }
        
        Destroy(this.gameObject);
    }


}
