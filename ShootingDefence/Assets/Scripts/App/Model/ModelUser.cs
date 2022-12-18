using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelUser
{
    public UnityAction onChanged;
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
        onChanged.Invoke();
    }

    public void HealHealth(float healAmount)
    {
        playerHealth += healAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
        onChanged.Invoke();
    }

    public void GetDamage(float damageAmount)
    {
        playerHealth -= damageAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
        onChanged.Invoke();
    }

    public float GetHpPercentage()
    {
        return (playerHealth / playerMaxHealth);
    }
}
