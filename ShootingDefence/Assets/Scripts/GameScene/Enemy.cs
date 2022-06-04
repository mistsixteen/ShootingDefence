using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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

    BulletInfo enemyBulletInfo;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        playerChar = GameObject.Find("Player");
        myRenderer = GetComponent<Renderer>();
        oriColor = myRenderer.material.color;
        healthBar = transform.Find("Healthbar/health").GetComponent<Image>();
        healthGrid = transform.Find("Healthbar").GetComponent<Transform>();
        SetBulletInfo();

        StartCoroutine(EnemyRoutine());
        StartCoroutine(BlinkRoutine());
        blinkFlag = false;
        hitVector = Vector3.zero;
    }

    void SetBulletInfo()
    {
        enemyBulletInfo.bulletSpeed = 0.5f;
        enemyBulletInfo.bulletDamage = 1.0f;
        enemyBulletInfo.bulletPushpower = 1.0f;
        enemyBulletInfo.bulletLifespan = 3000;
        enemyBulletInfo.bulletColor = Color.red;
        enemyBulletInfo.trailColor = Color.red;
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
            //Attack
            float distance = Vector3.Distance(this.transform.position, playerChar.transform.position);
            if(distance < 20.0f)
            {
                GameObject newBullet = BulletFactory.GetInstance().CreateBullet(enemyBulletInfo, BulletSpawn.position, BulletSpawn.rotation);
                newBullet.GetComponent<Bullet>().bulletFaction =  ObjectFaction.Enemy;

                yield return new WaitForSeconds(0.1f);
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

    public ObjectFaction getFaction()
    {
        return ObjectFaction.Enemy;
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

    public void Update()
    {
        //NavMesh에서 이동할 시 healthBar가 잠깐 휘어져 보이는 현상 해결
        //Update에서 rotation을 재조정해줘야 자연스럽게 보임
        //추후 해당 rotation 고정할 방법 찾아서 사용할 것
        healthGrid.rotation = Quaternion.Euler(70.0f, 0.0f, 0.0f);
    }
}
