using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayStateReady : GamePlayState
{
    private float timeLeft;

    public GamePlayStateReady()
    {
        gameState = StateGamePlay.StateGameReady;
    }

    public override void StartState()
    {
        Debug.Log("StartState : GameReady");
        timeLeft = GlobalCommonValues.GameStartWaitTime;
        SoundSystem.GetInstance().PlayBGM();
    }

    public override void EndState()
    {

    }

    public override void UpdateState(float timeDelta)
    {
        timeLeft -= timeDelta;

        if(timeLeft < 0)
        {
            GameStateManager.GetInstance()?.ChangeGameState(StateGamePlay.StateGameDay);
        }
    }
}
