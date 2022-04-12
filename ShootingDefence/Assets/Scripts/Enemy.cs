using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, Damageable
{
    private Coroutine eCoroutine, bCoroutine;
    public float moveSpeed;
    public int MaxHp = 100;
    public int currentHp = 100;
    GameObject player;
    Renderer render;
    Image healthBar;
    Rigidbody m_rigidbody;
    Color oriColor;
    bool isBlink = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        render = GetComponent<Renderer>();
        oriColor = render.material.color;
        healthBar = transform.Find("Healthbar/health").GetComponent<Image>();
        m_rigidbody = GetComponent<Rigidbody>();
        eCoroutine = StartCoroutine(EnemyCoroutine());
        bCoroutine = StartCoroutine(blinkCoroutine());

    }

    IEnumerator EnemyCoroutine()
    {
        Vector3 positionGap;
        Vector3 movepos;
        movepos.y = 0.0f;

        while (true)
        {
            positionGap = player.transform.position - transform.position;

            positionGap = positionGap.normalized;

            movepos.x = positionGap.x * moveSpeed;
            movepos.z = positionGap.z * moveSpeed;
            Debug.Log(positionGap);
            Debug.Log(movepos);
            m_rigidbody.MovePosition(m_rigidbody.position + movepos);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator blinkCoroutine()
    {
        while (true)
        {
            if (isBlink)
            {
                Debug.Log("true");
                render.material.color = Color.white;
                yield return new WaitForSeconds(0.01f);
                render.material.color = oriColor;
                isBlink = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void getDamage(int Damage)
    {
        Debug.Log("Bullet Hit!!" + currentHp);
        isBlink = true;

        currentHp -= Damage;
        healthBar.fillAmount = (float)currentHp / MaxHp;
        if (currentHp < 0)
            Destroy(this.gameObject);
    }
}
