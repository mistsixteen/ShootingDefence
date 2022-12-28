using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayStateNight : GamePlayState
{
    private float timeLeft;
    private float SpawnTimeLeft;

    public GamePlayStateNight()
    {
        gameState = StateGamePlay.StateGameNight;
    }

    public override void StartState()
    {
        Debug.Log("StartState : GameNight");
        timeLeft = GlobalCommonValues.GameDayLength;
        SpawnTimeLeft = 5.0f;
    }

    public override void EndState()
    {

    }

    public override void UpdateState(float timeDelta)
    {
        timeLeft -= timeDelta;

        if (timeLeft < 0)
        {
            GameStateManager.GetInstance()?.ChangeGameState(StateGamePlay.StateGameDay);
        }
        if (SpawnTimeLeft < 0)
        {
            EnemySpawner.GetInstance()?.SpawnEnemyInRandomPoint();
            SpawnTimeLeft = 5.0f;
        }
    }
}
