using System;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    public event Action OnExitJump;
    public event Action OnExitSlide;

    [SerializeField] private PlayerInputSystem _inputSystem;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.SetBool("Running", true);
    }

    private void OnEnable()
    {
        _inputSystem.JumpPerformed += Jump;
        _inputSystem.SlidePerformed += Slide;
    }

    private void OnDisable()
    {
        _inputSystem.JumpPerformed -= Jump;
        _inputSystem.SlidePerformed -= Slide;
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    private void Slide()
    {
        _animator.SetTrigger("Slide");
    }

    private void ExitJump()
    {
        OnExitJump?.Invoke();
    }

    private void ExitSlide()
    {
        OnExitSlide?.Invoke();
    }
}
