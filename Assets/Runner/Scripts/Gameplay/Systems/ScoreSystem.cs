using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public float CurrentScore { get; private set; }

    [SerializeField] private GroundSpeedSystem _groundSpeedSystem;
    [SerializeField] private float _speedMultiplyer = 0.5f;

    private float _groundSpeed;

    private void Start()
    {
        _groundSpeed = _groundSpeedSystem.GroundModel.CurrentSpeed;    
    }

    private void Update()
    {
        CurrentScore += _groundSpeed * Time.deltaTime * _speedMultiplyer;

        _groundSpeed = _groundSpeedSystem.GroundModel.CurrentSpeed;
    }

    public int GetScoreToInt()
    {
        return Mathf.FloorToInt(CurrentScore);
    }
}
