using UnityEngine;

public enum EnumGameScene
{
    SceneGameStart,
    SceneMainMenu,
    SceneGameReady,
    SceneGame,
    SceneGameOver,
    Max
}

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

public enum ItemState
{
    ItemStateNull,
    ItemStateIdle,
    ItemStateGunReloading,
}

public enum ItemType
{
    ItemTypeNull,
    ItemTypeGoods,
    ItemTypeWeapon,

}