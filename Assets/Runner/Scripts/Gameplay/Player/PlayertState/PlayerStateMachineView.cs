using UnityEngine;

public class PlayerStateMachineView : MonoBehaviour
{
    public LaneSystem LaneSystem => _laneSystem;
    public MovementSystem MovementSystem => _movementSystem;
    public PlayerCollisionSystem PlayerColliisionSystem => _playerColliisionSystem;
    public RunningState RunningState => _runningState;
    public ChangeLaneState ChangeLaneState => _changeLaneState;
    public JumpState JumpState => _jumpState;
    public SlideState SlideState => _slideState;
    public PlayerAnimationSystem AnimationSystem => _playerAnimationSystem;

    [SerializeField] private PlayerInputSystem _inputSystem;
    [SerializeField] private LaneSystem _laneSystem;
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private PlayerCollisionSystem _playerColliisionSystem;
    [SerializeField] private PlayerAnimationSystem _playerAnimationSystem;

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
        _inputSystem.LeftPerformed += OnLeft;
        _inputSystem.RightPerformed += OnRight;
        _inputSystem.JumpPerformed += OnJump;
        _inputSystem.SlidePerformed += OnSlide;

        _playerAnimationSystem.OnExitJump += OnAnimationFinished;
        _playerAnimationSystem.OnExitSlide += OnAnimationFinished;

    }

    private void OnDisable()
    {
        _inputSystem.LeftPerformed -= OnLeft;
        _inputSystem.RightPerformed -= OnRight;
        _inputSystem.JumpPerformed -= OnJump;
        _inputSystem.SlidePerformed -= OnSlide;

        _playerAnimationSystem.OnExitJump -= OnAnimationFinished;
        _playerAnimationSystem.OnExitSlide -= OnAnimationFinished;
    }

    private void OnAnimationFinished()
    {
        _currentState.OnAnimationFinished(this);
    }

    private void OnLeft()
    {
        _currentState.OnLeft(this);
    }

    private void OnRight()
    {
        _currentState.OnRight(this);
    }

    private void OnJump()
    {
        _currentState.OnJump(this);
    }

    private void OnSlide()
    {
        _currentState.OnSlide(this);
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
