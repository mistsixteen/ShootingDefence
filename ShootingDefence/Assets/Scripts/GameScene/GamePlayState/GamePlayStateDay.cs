using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayStateDay : GamePlayState
{
    private float timeLeft;
    private float SpawnTimeLeft;

    public GamePlayStateDay()
    {
        gameState = StateGamePlay.StateGameDay;
    }

    public override void StartState()
    {
        Debug.Log("StartState : GameDay");
        timeLeft = GlobalCommonValues.GameDayLength;
        SpawnTimeLeft = 5.0f;
    }

    public override void EndState()
    {

    }

    public override void UpdateState(float timeDelta)
    {
        timeLeft -= timeDelta;
        SpawnTimeLeft -= timeDelta;

        if (timeLeft < 0)
        {
            GameStateManager.GetInstance()?.ChangeGameState(StateGamePlay.StateGameNight);
        }

        if(SpawnTimeLeft < 0)
        {
            EnemySpawner.GetInstance()?.SpawnEnemyInRandomPoint();
            SpawnTimeLeft = 5.0f;
        }
    }
}
