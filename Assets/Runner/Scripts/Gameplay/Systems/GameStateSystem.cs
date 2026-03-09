using System;
using UnityEngine;

public class GameStateSystem : MonoBehaviour
{
    public EGameState CurrentState { get; private set; }

    public event Action OnDeath;
    public event Action OnRevive;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void KillPlayer()
    {
        if (CurrentState != EGameState.Playing)
        {
            return;
        }

        CurrentState = EGameState.Dead;

        Time.timeScale = 0f;

        OnDeath?.Invoke();
    }

    public void RevivePlayer()
    {
        OnRevive?.Invoke();

        CurrentState = EGameState.Playing;

        Time.timeScale = 1f;

    }

    public void StartGame()
    {
        CurrentState = EGameState.Playing;
        Time.timeScale = 1f;
    }
}
