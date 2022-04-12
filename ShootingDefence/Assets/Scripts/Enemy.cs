using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, Damageable
{
    public float moveSpeed;
    
    public float maxHp = 100;
    public float currentHp;
    
    GameObject playerChar;

    Renderer myRenderer;
    Rigidbody myRigidbody;
    Image healthBar;
    
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
        myRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(EnemyRoutine());
        StartCoroutine(BlinkRoutine());
        blinkFlag = false;
        hitVector = Vector3.zero;

    }

    IEnumerator EnemyRoutine()
    {
        Vector3 positionGap;
        Vector3 movepos;
        movepos.y = 0.0f;

        while (true)
        {
            positionGap = playerChar.transform.position - transform.position;

            positionGap = positionGap.normalized;

            movepos.x = positionGap.x * moveSpeed;
            movepos.z = positionGap.z * moveSpeed;
            myRigidbody.MovePosition(myRigidbody.position + movepos + hitVector);
            hitVector = Vector3.zero;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            if (blinkFlag)
            {
                Debug.Log("true");
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
        Debug.Log("Bullet Hit!!" + currentHp + " " + Direction * pushPower);
        blinkFlag = true;

        hitVector += Direction * pushPower;

        currentHp -= Damage;
        healthBar.fillAmount = (float)currentHp / maxHp;
        if (currentHp < 0)
            Destroy(this.gameObject);
    }
}
