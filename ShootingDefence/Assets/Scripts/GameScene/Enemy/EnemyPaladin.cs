using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyPaladin : MonoBehaviour, Damageable
{
    public float moveSpeed;

    public float maxHp = 50;
    public float currentHp;
    public Animator myanimator;

    GameObject playerChar;
    Image healthBar;
    Transform healthGrid;
    

    Color oriColor;
    Vector3 hitVector;
    Collider myCollider;

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
            if (distance < 2.0f)
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
        myanimator.Play("Attack");
        yield return new WaitForSeconds(0.01f);
        agent.isStopped = true;
        while (true)
        {
            //Move
            agent.Move(hitVector);
            hitVector = Vector3.zero;
            //Attack
            float distance = Vector3.Distance(this.transform.position, playerChar.transform.position);
            if (distance > 3.0f)
            {
                ChangeCoroutine(MoveCoroutine());
                yield return new WaitForSeconds(0.1f);
            }
            if(myanimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if(playerChar.TryGetComponent(out Damageable dObject))
                {
                    dObject.GetDamage(5.0f, 2.0f, Vector3.zero);
                }
                yield return new WaitForSeconds(0.5f);
                ChangeCoroutine(MoveCoroutine());
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    //onDeath
    IEnumerator OnDeathCoroutine()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        myanimator.Play("Death");
        healthGrid.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        myCollider.enabled = false;
        agent.isStopped = true;
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
        //���� �ش� rotation ������ ��� ã�Ƽ� ����� ��
        healthGrid.rotation = Quaternion.Euler(70.0f, 0.0f, 0.0f);
    }

}
