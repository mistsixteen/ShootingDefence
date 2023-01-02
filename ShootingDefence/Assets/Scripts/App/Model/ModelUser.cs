using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelUser
{
    private float playerHealth;
    private float playerMaxHealth;

    public ModelUser()
    {
        this.InitializeUserData();
    }

    public void InitializeUserData()
    {
        playerHealth = GlobalCommonValues.UserMaxHealth;
        playerMaxHealth = GlobalCommonValues.UserMaxHealth;
        EventSystem.GetInstance()?.InvokeEvent(EventType.onModelUserChanged);
    }

    public void HealHealth(float healAmount)
    {
        playerHealth += healAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
        EventSystem.GetInstance()?.InvokeEvent(EventType.onModelUserChanged);
    }

    public void GetDamage(float damageAmount)
    {
        playerHealth -= damageAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0.0f, playerMaxHealth);
        EventSystem.GetInstance()?.InvokeEvent(EventType.onModelUserChanged);
        if(playerHealth == 0.0f)
            EventSystem.GetInstance()?.InvokeEvent(EventType.onPlayerDead);
    }

    public float GetHpPercentage()
    {
        return (playerHealth / playerMaxHealth);
    }
}
