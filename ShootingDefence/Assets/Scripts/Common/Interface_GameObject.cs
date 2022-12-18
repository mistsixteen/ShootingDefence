using UnityEngine;

public interface Damageable
{
    public void GetDamage(float Damage, float pushPower, Vector3 Direction);
    public ObjectFaction getFaction();
}
