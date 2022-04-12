using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, Damageable
{
    private Coroutine eCoroutine, bCoroutine;
    public float moveSpeed;
    public float MaxHp = 100;
    float currentHp;
    GameObject player;
    Renderer render;
    Image healthBar;
    Rigidbody m_rigidbody;
    Color oriColor;
    Vector3 fallback;
    bool isBlink;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = MaxHp;
        player = GameObject.Find("Player");
        render = GetComponent<Renderer>();
        oriColor = render.material.color;
        healthBar = transform.Find("Healthbar/health").GetComponent<Image>();
        m_rigidbody = GetComponent<Rigidbody>();
        eCoroutine = StartCoroutine(EnemyCoroutine());
        bCoroutine = StartCoroutine(blinkCoroutine());
        isBlink = false;
        fallback = Vector3.zero;

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
            m_rigidbody.MovePosition(m_rigidbody.position + movepos + fallback);
            fallback = Vector3.zero;
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

    public void getDamage(float Damage, float pushPower, Vector3 Direction)
    {
        Debug.Log("Bullet Hit!!" + currentHp + " " + Direction * pushPower);
        isBlink = true;

        fallback += Direction * pushPower;

        currentHp -= Damage;
        healthBar.fillAmount = (float)currentHp / MaxHp;
        if (currentHp < 0)
            Destroy(this.gameObject);
    }
}
