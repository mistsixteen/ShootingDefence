using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public enum EnemyState
{
    eStateFinding,
    eStateAttacking,
    eStateAfterAttack
}

public class Enemy : MonoBehaviour, Damageable
{
    public float moveSpeed;
    
    public float maxHp = 100;
    public float currentHp;
    
    GameObject playerChar;
    Renderer myRenderer;
    Image healthBar;
    Transform healthGrid;
    public Transform BulletSpawn;
    public GameObject Bullet;

    Color oriColor;
    Vector3 hitVector;
    bool blinkFlag;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        playerChar = GameObject.Find("Player");
        myRenderer = GetComponent<Renderer>();
        oriColor = myRenderer.material.color;
        healthBar = transform.Find("Healthbar/health").GetComponent<Image>();
        healthGrid = transform.Find("Healthbar").GetComponent<Transform>();
        StartCoroutine(EnemyRoutine());
        StartCoroutine(BlinkRoutine());
        blinkFlag = false;
        hitVector = Vector3.zero;

    }

    IEnumerator EnemyRoutine()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        while (true)
        {
            //Move-rotate to Player
            agent.destination = playerChar.transform.position;
            agent.Move(hitVector);
            hitVector = Vector3.zero;
            healthGrid.rotation = Quaternion.Euler(70.0f, 0.0f, 0.0f);
            //Attack
            float distance = Vector3.Distance(this.transform.position, playerChar.transform.position);
            Debug.Log(distance);
            if(distance < 20.0f)
            {
                GameObject newBullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                newBullet.GetComponent<Bullet>().bulletSpeed = 0.5f;
                //newBullet.GetComponent<Bullet>().
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            if (blinkFlag)
            {
                myRenderer.material.color = Color.white;
                yield return new WaitForSeconds(0.01f);
                myRenderer.material.color = oriColor;
                blinkFlag = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GetDamage(float Damage, float pushPower, Vector3 Direction)
    {
        blinkFlag = true;

        hitVector += Direction * pushPower;

        currentHp -= Damage;
        healthBar.fillAmount = (float)currentHp / maxHp;
        if (currentHp < 0)
            Destroy(this.gameObject);
    }
}
