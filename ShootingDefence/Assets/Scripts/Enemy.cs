using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    public int moveSpeed;
    public int currentHp = 100;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionGap = player.transform.position - transform.position;
        
        positionGap = positionGap.normalized;
        
        transform.Translate(positionGap.x * Time.deltaTime * moveSpeed,
                            0f,
                            positionGap.z * Time.deltaTime * moveSpeed);
    }

    public void getDamage(int Damage)
    {

        Debug.Log("Bullet Hit!!" + currentHp);
        currentHp -= Damage;
        if (currentHp < 0)
            Destroy(this.gameObject);
    }
}
