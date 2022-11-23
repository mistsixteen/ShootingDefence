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

    public ObjectFaction getFaction()
    {
        return ObjectFaction.Ally;
    }

    public void getHealthPack()
    {
        //TODO : 추후 Model에 맞춰 수정
        AppInstance.GetInstance().ModelManager.ModelUser.HealHealth(30);
    }

    public void GetDamage(float Damage, float pushPower, Vector3 Direction)
    {
        AppInstance.GetInstance().ModelManager.ModelUser.GetDamage(Damage);

    }
}
