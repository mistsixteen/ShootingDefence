using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayStateGameOver : GamePlayState
{
    public GamePlayStateGameOver()
    {
        gameState = StateGamePlay.StateGameOver;
    }

    public override void StartState()
    {
        AppInstance.GetInstance().ChangeCurrentScene(EnumGameScene.SceneGameOver);
    }

    public override void EndState()
    {

    }

    public override void UpdateState(float timeDelta)
    {

    }
}
