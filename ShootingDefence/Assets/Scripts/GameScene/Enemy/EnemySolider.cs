using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemySolider : MonoBehaviour, Damageable
{
    public float moveSpeed;

    public float maxHp = 50;
    public float currentHp;
    public Animator myanimator;

    GameObject playerChar;
    Image healthBar;
    Transform healthGrid;
    Collider myCollider;

    Color oriColor;
    Vector3 hitVector;

    private TableProjectileRow tableRow;

    public Transform BulletSpawn;

    private Coroutine currentCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        playerChar = GameObject.Find("Player");
        healthBar = transform.Find("Healthbar/health").GetComponent<Image>();
        healthGrid = transform.Find("Healthbar").GetComponent<Transform>();
        myCollider = GetComponent<Collider>();
        hitVector = Vector3.zero;
        tableRow = AppInstance.GetInstance().TableManager.TableProjectile.GetTableRow(3);
        StartCoroutine(IdleCoroutine());
    }

    private void ChangeCoroutine(IEnumerator nextCoroutine)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(nextCoroutine);
    }

    //idle
    IEnumerator IdleCoroutine()
    {
        ChangeCoroutine(MoveCoroutine());
        yield return null;
    }

    //move
    IEnumerator MoveCoroutine()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        myanimator.Play("Run");
        yield return new WaitForSeconds(0.01f);
        agent.isStopped = false;

        while (true)
        {
            //Move-rotate to Player
            agent.destination = playerChar.transform.position;
            agent.Move(hitVector);
            hitVector = Vector3.zero;
            //Attack
            float distance = Vector3.Distance(this.transform.position, playerChar.transform.position);
            if (distance < 10.0f)
            {

                ChangeCoroutine(AttackingCoroutine());
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    //attacking
    IEnumerator AttackingCoroutine()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        myanimator.Play("Fire");
        yield return new WaitForSeconds(0.01f);
        agent.isStopped = true;
        while (true)
        {
            //Move
            agent.Move(hitVector);
            hitVector = Vector3.zero;

            //rotate only
            var Direction = (playerChar.transform.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.5f);

            //Attack
            float distance = Vector3.Distance(this.transform.position, playerChar.transform.position);
            if (distance > 10.0f)
            {
                ChangeCoroutine(MoveCoroutine());
                yield return new WaitForSeconds(0.1f);
            }
            if (myanimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                var newBullet = BulletFactory.GetInstance().CreateBullet(tableRow, BulletSpawn.position, BulletSpawn.rotation);
                newBullet.bulletFaction = ObjectFaction.Enemy;
                yield return new WaitForSeconds(0.1f);
                ChangeCoroutine(MoveCoroutine());
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    //onDeath
    IEnumerator OnDeathCoroutine()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        yield return new WaitForSeconds(0.01f);
        agent.isStopped = true;
        myCollider.enabled = false;
        myanimator.Play("Death");
        healthGrid.gameObject.SetActive(false);
        while (true)
        {
            if (myanimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public ObjectFaction getFaction()
    {
        return ObjectFaction.Enemy;
    }

    public void GetDamage(float Damage, float pushPower, Vector3 Direction)
    {
        hitVector += Direction * pushPower;

        currentHp -= Damage;
        healthBar.fillAmount = (float)currentHp / maxHp;
        if (currentHp < 0)
        {
            ChangeCoroutine(OnDeathCoroutine());
        }

    }

    public void Update()
    {
        //추후 해당 rotation 고정할 방법 찾아서 사용할 것
        healthGrid.rotation = Quaternion.Euler(70.0f, 0.0f, 0.0f);
    }



}
