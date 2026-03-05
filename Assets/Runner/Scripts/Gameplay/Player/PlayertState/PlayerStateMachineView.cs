using UnityEngine;

public class PlayerStateMachineView : MonoBehaviour
{
    public LaneSystem LaneSystem => _laneSystem;
    public MovementSystem MovementSystem => _movementSystem;
    public RunningState RunningState => _runningState;
    public ChangeLaneState ChangeLaneState => _changeLaneState;
    public JumpState JumpState => _jumpState;
    public SlideState SlideState => _slideState;

    [SerializeField] private PlayerInputSystem _inputSystem;
    [SerializeField] private LaneSystem _laneSystem;
    [SerializeField] private MovementSystem _movementSystem;

    private PlayerState _currentState;

    private RunningState _runningState = new RunningState();
    private ChangeLaneState _changeLaneState = new ChangeLaneState();
    private JumpState _jumpState = new JumpState();
    private SlideState _slideState = new SlideState();

    private void Start()
    {
        _currentState = _runningState;
        _currentState.EnterState(this);
    }

    private void OnEnable()
    {
        _inputSystem.LeftPerformed += () => _currentState.OnLeft(this);
        _inputSystem.RightPerformed += () => _currentState.OnRight(this);
        _inputSystem.JumpPerformed += () => _currentState.OnJump(this);
        _inputSystem.SlidePerformed += () => _currentState.OnSlide(this);
    }

    private void OnDisable()
    {
        _inputSystem.LeftPerformed -= () => _currentState.OnLeft(this);
        _inputSystem.RightPerformed -= () => _currentState.OnRight(this);
        _inputSystem.JumpPerformed -= () => _currentState.OnJump(this);
        _inputSystem.SlidePerformed -= () => _currentState.OnSlide(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(PlayerState newState)
    {
        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }
}
