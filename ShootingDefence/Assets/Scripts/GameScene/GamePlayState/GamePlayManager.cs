using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private GamePlayState currentState;
    private Dictionary<StateGamePlay, GamePlayState> dictPlayStates;
    private void Awake()
    {
        InitializeGamePlay();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(Time.deltaTime);
    }

    private void InitializeGamePlay()
    {
        dictPlayStates = new Dictionary<StateGamePlay, GamePlayState>();
    }

    private void changeGameState(StateGamePlay targetState)
    {
        if (currentState.gameState == targetState) { return; }
    }
}
