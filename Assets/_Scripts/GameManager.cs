using System;
using UnityEngine;

public static class GameManager {
    public static event Action<GameState> OnGameStateChanged;
    public static GameState CurrentState { private set; get; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() {
        CurrentState = GameState.None;
    }

    public static void ChangeState(GameState newState) {
        if (CurrentState == newState) return;
        CurrentState = newState;

        //Show ui, sounds or calculations etc 
        switch (CurrentState) {
            case GameState.None:
                break;
            case GameState.Menu:
                break;
            case GameState.Initialize:
                break;
            case GameState.InGame:
                break;
            case GameState.Pause:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
        OnGameStateChanged?.Invoke(CurrentState);
    }
}

public enum GameState {
    None,
    Menu,
    Initialize,
    InGame,
    Pause,
    Win,
    Lose,
}