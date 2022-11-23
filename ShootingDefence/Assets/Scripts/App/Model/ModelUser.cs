using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelUser
{
    private float playerHealth;
    private float playerMaxHealth;

    public ModelUser()
    {
        playerHealth = 100;
        playerMaxHealth = 100;
    }

    public void InitializeUserData(float startMaxHealth)
    {
        playerHealth = startMaxHealth;
        playerMaxHealth = startMaxHealth;
    }

    public void HealHealth(float healAmount)
    {
        playerHealth += healAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
    }

    public void GetDamage(float damageAmount)
    {
        playerHealth -= damageAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
    }

    public float GetHpPercentage()
    {
        return (playerHealth / playerMaxHealth);
    }
}
