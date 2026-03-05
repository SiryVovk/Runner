using System;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    public event Action LeftPerformed;
    public event Action RightPerformed;
    public event Action SlidePerformed;
    public event Action JumpPerformed;

    private IPlayerInputStrategy _inputStrategy;

    private void Awake()
    {
#if UNITY_EDITOR
        _inputStrategy = new EditorInputStrategy();
#elif UNITY_ANDROID
        _inputStrategy = new BuildInputStrategy();
#endif

        _inputStrategy.LeftPerformed += () => LeftPerformed?.Invoke();
        _inputStrategy.RightPerformed += () => RightPerformed?.Invoke();
        _inputStrategy.SlidePerformed += () => SlidePerformed?.Invoke();
        _inputStrategy.JumpPerjormed += () => JumpPerformed?.Invoke();
    }

    private void OnEnable()
    {
        _inputStrategy.Enable();
    }

    private void OnDisable()
    {
        _inputStrategy.Disable();
    }
}