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

public enum ItemType
{
    ItemTypeNull,
    ItemTypeGoods,
    ItemTypeWeapon,
}

public enum EventType
{
    //Model
    onModelInvenChanged,
    onModelUserChanged,

    //Reload
    onReloadStarted,
    onReloadFinished,

    //Player
    onPlayerDead,

    //GamePlayState
    onGamePlayStateChanged,
}

public enum StateGamePlay
{
    StateGameReady,
    StateGameDay,
    StateGameNight,
    StateGameOver,
    Max
}