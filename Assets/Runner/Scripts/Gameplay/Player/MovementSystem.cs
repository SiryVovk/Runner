using System.Collections;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool IsSliding { get; private set; }

    [Header("LanesChange")]
    [SerializeField] private float _changeLaneSpeed = 2f;
    [SerializeField] private Transform[] _laneTransforms;
    [SerializeField] private LaneSystem _laneSystem;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;

    [Header("Slide")]
    [SerializeField] private float _slideTime = 5f;
    [SerializeField] private Collider _normalCollider;
    [SerializeField] private Collider _slidingCollider;

    private Rigidbody _rigidbody;
    private Vector3 _targetPosition;

    private Coroutine _slideRoutine;

    private const float DistanceGroundCheck = 1.1f;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateTargetPosition();
        MoveToTargetPosition();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void UpdateTargetPosition()
    {
        int lane = _laneSystem.LaneModel.CurrentLane;
        
        _targetPosition = new Vector3(_laneTransforms[lane].position.x, transform.position.y, transform.position.z);
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _changeLaneSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(Vector3.up *  _jumpForce, ForceMode.Impulse);
    }

    private void CheckGround()
    {
        IsGrounded = Physics.Raycast(_normalCollider.bounds.center, Vector3.down, DistanceGroundCheck);
    }
    public bool IsFalling()
    {
        return _rigidbody.linearVelocity.y < 0f;
    }

    public bool IsCenteredOnLane()
    {
        int lane = _laneSystem.LaneModel.CurrentLane;
        float targetX = _laneTransforms[lane].position.x;

        return Mathf.Abs(transform.position.x - targetX) < 0.05f;
    }

    public void Slide()
    {
        _normalCollider.enabled = false;
        _slidingCollider.enabled = true;

        IsSliding = true;

        if(_slideRoutine != null)
        {
            StopCoroutine(_slideRoutine);
        }

        _slideRoutine = StartCoroutine(SlideRoutine());
    }

    private IEnumerator SlideRoutine()
    {
        yield return new WaitForSeconds(_slideTime);

        IsSliding = false;
        _normalCollider.enabled = true;
        _slidingCollider.enabled = false;

        _slideRoutine = null;
    }
}
