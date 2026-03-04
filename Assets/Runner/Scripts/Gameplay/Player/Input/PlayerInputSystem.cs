using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    private IPlayerInputStrategy _inputStrategy;

    private void Awake()
    {
#if UNITY_EDITOR
        _inputStrategy = new BuildInputStrategy();
        //_inputStrategy = new EditorInputStrategy();
#elif UNITY_ANDROID
        _inputStrategy = new BuildInputStrategy();
#endif
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