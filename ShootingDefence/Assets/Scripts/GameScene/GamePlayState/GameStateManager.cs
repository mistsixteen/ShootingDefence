using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance = null;

    private GamePlayState currentState = null;
    private Dictionary<StateGamePlay, GamePlayState> dictGameStates;
    private void Awake()
    {
        Instance = this;
        InitializeGamePlay();
    }

    public static GameStateManager GetInstance()
    {
        return Instance;
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(Time.deltaTime);
    }

    private void InitializeGamePlay()
    {
        dictGameStates = new Dictionary<StateGamePlay, GamePlayState>
        {
            { StateGamePlay.StateGameReady, new GamePlayStateReady() as GamePlayState },
            { StateGamePlay.StateGameDay, new GamePlayStateDay() as GamePlayState },
            { StateGamePlay.StateGameNight, new GamePlayStateNight() as GamePlayState },
            { StateGamePlay.StateGameOver, new GamePlayStateNight() as GamePlayState }
        };
        ChangeGameState(StateGamePlay.StateGameReady);
    }

    public void ChangeGameState(StateGamePlay targetState)
    {
        if(currentState != null && currentState.gameState == targetState) { return; }
        if(currentState != null)
        {
            currentState.EndState();
        }

        if(dictGameStates[targetState] != null)
        { 
            currentState = dictGameStates[targetState];
            currentState.StartState();
        }
    }
}
