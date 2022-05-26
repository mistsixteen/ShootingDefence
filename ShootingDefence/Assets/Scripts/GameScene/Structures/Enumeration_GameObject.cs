using UnityEngine;


public enum ObjectFaction
{
    Ally,
    Enemy,
    Normal
}

public enum EnemyState
{
    eStateFinding,
    eStateAttacking,
    eStateAfterAttack
}

public enum gunState
{
    gunStateEmpty,
    gunStateIdle,
    gunStateReloading
}
