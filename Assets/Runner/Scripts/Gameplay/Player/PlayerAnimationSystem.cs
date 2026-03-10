using System;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    public event Action OnExitJump;
    public event Action OnExitSlide;

    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.SetBool("Running", true);
    }


    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
    }
    public void TriggerSlide()
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
