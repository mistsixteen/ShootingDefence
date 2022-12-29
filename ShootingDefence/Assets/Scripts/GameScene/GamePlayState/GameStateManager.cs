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

    private void OnEnable()
    {
        EventSystem.GetInstance()?.RegistEventListener(EventType.onPlayerDead, OnPlayerDead);
    }
    private void OnDisable()
    {
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onPlayerDead, OnPlayerDead);
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
            { StateGamePlay.StateGameOver, new GamePlayStateGameOver() as GamePlayState }
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
            EventSystem.GetInstance()?.InvokeEvent(EventType.onGamePlayStateChanged);
        }
    }
    public StateGamePlay GetCurrentState()
    {
        return currentState.gameState;
    }

    public void OnPlayerDead()
    {
        if(currentState.gameState != StateGamePlay.StateGameOver)
            ChangeGameState(StateGamePlay.StateGameOver);
    }
}
