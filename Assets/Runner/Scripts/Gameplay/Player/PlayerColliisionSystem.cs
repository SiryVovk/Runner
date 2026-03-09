using System.Collections;
using UnityEngine;

public class PlayerColliisionSystem : MonoBehaviour
{
    public bool IsSliding {  get; private set; }
    public bool IsJumping { get; private set; }

    [Header ("References")]
    [SerializeField] private GameStateSystem _gameStateSystem;
    [SerializeField] private PlayerAnimationSystem _playerAnimationSystem;
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    [SerializeField] private Collider _playerCollider;

    [Header("Setings")]
    [SerializeField] private float _invincibilityTime = 2f;

    private bool _isInvincible = false;
    private bool _isIgnoringCollision = false;

    private void OnEnable()
    {
        _gameStateSystem.OnRevive += Reviveed;
        _playerInputSystem.JumpPerformed += Jump;
        _playerInputSystem.SlidePerformed += Slide;
        _playerAnimationSystem.OnExitJump += StopJump;
        _playerAnimationSystem.OnExitSlide += StopSlide;
    }

    private void OnDisable()
    {
        _gameStateSystem .OnRevive -= Reviveed;
        _playerInputSystem.JumpPerformed -= Jump;
        _playerInputSystem.SlidePerformed -= Slide;
        _playerAnimationSystem.OnExitJump -= StopJump;
        _playerAnimationSystem.OnExitSlide -= StopSlide;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled)
        {
            return;
        }

        if (_isIgnoringCollision || _isInvincible)
        {
            return;
        }

        if(collision.gameObject.TryGetComponent(out ObstacleHolder obstacle))
        {
            _gameStateSystem.KillPlayer();
        }
    }

    private void Reviveed()
    {
        _isInvincible = true;
        StartCoroutine(InvincibilytyRoutine());
    }

    private IEnumerator InvincibilytyRoutine()
    {

        yield return new WaitForSeconds(_invincibilityTime);

        _isInvincible = false;
    }

    private void Jump()
    {
        DisableCollision();
    }

    private void StopJump()
    {
        EnableColision();
    }

    private void Slide()
    {
        DisableCollision();
    }

    private void StopSlide()
    {
        EnableColision();
    }

    private void DisableCollision()
    {
        _playerCollider.isTrigger = true;
        _isIgnoringCollision = true;
    }

    private void EnableColision()
    {
        _playerCollider.isTrigger = false;
        _isIgnoringCollision = false;
    }
}
