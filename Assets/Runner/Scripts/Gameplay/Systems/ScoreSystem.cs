using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public float CurrentScore { get; private set; }

    [SerializeField] private GroundSpeedSystem _groundSpeedSystem;
    [SerializeField] private GameStateSystem _gameStateSystem;
    [SerializeField] private float _speedMultiplier = 0.5f;

    private void Update()
    {
        if (_gameStateSystem.CurrentState != EGameState.Playing)
        {
            return;
        }

        float currentSpeed = _groundSpeedSystem.GroundModel.CurrentSpeed;
        CurrentScore += currentSpeed * Time.deltaTime * _speedMultiplier;

    }

    public int GetScoreToInt()
    {
        return Mathf.FloorToInt(CurrentScore);
    }
}
