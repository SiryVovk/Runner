using System.Collections;
using UnityEngine;

public class PlayerColliisionSystem : MonoBehaviour
{
    [SerializeField] private GameStateSystem _gameStateSystem;
    [SerializeField] private float _invincibilityTime = 2f;

    private bool _isInvincible = false;

    private void OnEnable()
    {
        _gameStateSystem.OnRevive += Reviveed;
    }

    private void OnDisable()
    {
        _gameStateSystem  .OnRevive -= Reviveed; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isInvincible)
        {
            _isInvincible = false;
        }

        ObstacleHolder obstacleHolder = collision.gameObject.GetComponent<ObstacleHolder>();

        if(obstacleHolder != null )
        {
            _gameStateSystem.KillPlayer();
        }
    }

    private void Reviveed()
    {
        StartCoroutine(InvincibilytyRoutine());
    }

    private IEnumerator InvincibilytyRoutine()
    {
        _isInvincible = true;

        yield return new WaitForSeconds(_invincibilityTime);

        _isInvincible = false;
    }
}
