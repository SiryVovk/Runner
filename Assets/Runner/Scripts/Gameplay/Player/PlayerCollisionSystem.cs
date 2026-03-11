using System.Collections;
using UnityEngine;

public class PlayerCollisionSystem : MonoBehaviour
{
    public bool IsSliding { get; private set; }
    public bool IsJumping { get; private set; }

    [Header("References")]
    [SerializeField] private GameStateSystem _gameStateSystem;

    [Header("Settings")]
    [SerializeField] private float _invincibilityTime = 2f;

    private bool _isInvincible = false;

    private void OnEnable()
    {
        _gameStateSystem.OnRevive += Revived;
    }

    private void OnDisable()
    {
        _gameStateSystem.OnRevive -= Revived;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || _isInvincible)
        {
            return;
        }

        if (other.TryGetComponent(out ObstacleHolder obstacle))
        {
            bool survives = obstacle.ObstacleType switch
            {
                EObstacleType.Wall => false,
                EObstacleType.Jump => IsJumping,
                EObstacleType.Slide => IsSliding,
                _ => false
            };

            if (!survives)
            {
                _gameStateSystem.KillPlayer();
            }
        }
    }

    public void SetJumping(bool state)
    {
        IsJumping = state;
    }

    public void SetSliding(bool state)
    {
        IsSliding = state;
    }

    private void Revived()
    {
        _isInvincible = true;
        StartCoroutine(InvincibilytyRoutine());
    }

    private IEnumerator InvincibilytyRoutine()
    {
        yield return new WaitForSeconds(_invincibilityTime);
        _isInvincible = false;
    }
}
