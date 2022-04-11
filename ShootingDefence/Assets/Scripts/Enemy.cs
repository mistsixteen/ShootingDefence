using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    private Coroutine eCoroutine, bCoroutine;
    public float moveSpeed;
    public int currentHp = 100;
    GameObject player;
    Renderer render;
    Color oriColor;
    bool isBlink = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        render = GetComponent<Renderer>();
        oriColor = render.material.color;
        eCoroutine = StartCoroutine(EnemyCoroutine());
        bCoroutine = StartCoroutine(blinkCoroutine());
    }

    IEnumerator EnemyCoroutine()
    {
        Vector3 positionGap;

        while (true)
        {
            positionGap = player.transform.position - transform.position;

            positionGap = positionGap.normalized;

            transform.Translate(positionGap.x * Time.deltaTime * moveSpeed,
                                0f,
                                positionGap.z * Time.deltaTime * moveSpeed);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator blinkCoroutine()
    {
        while (true)
        {
            if (isBlink)
            {
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
        if (currentHp < 0)
            Destroy(this.gameObject);
    }
}
