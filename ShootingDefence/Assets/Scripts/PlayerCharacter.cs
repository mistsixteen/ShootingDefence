using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, Damageable
{
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

    public void GetDamage(float Damage, float pushPower, Vector3 Direction)
    {
        Debug.Log("GetDamaged");
    }
}
