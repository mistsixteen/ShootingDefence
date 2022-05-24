using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, Damageable
{
    public float playerHealth = 100;
    public float playerMaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealthBy(float damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
        //todo:Change Helath UI
    }

    public ObjectFaction getFaction()
    {
        return ObjectFaction.Ally;
    }

    public float getHpPercentage()
    {
        float fillAmount = (playerHealth / playerMaxHealth);
        return (playerHealth / playerMaxHealth);
    }

    public void GetDamage(float Damage, float pushPower, Vector3 Direction)
    {
        ChangeHealthBy(Damage);
    }
}
