using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePlayState
{
    public StateGamePlay gameState;

    public virtual void StartState()
    {

    }

    public virtual void EndState()
    {

    }

    public virtual void UpdateState(float timeDelta)
    {

    }
}
