using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [Header("LanesChange")]
    [SerializeField] private float _changeLaneSpeed = 2f;
    [SerializeField] private Transform[] _laneTransforms;
    [SerializeField] private LaneSystem _laneSystem;

    private Vector3 _targetPosition;

    private const float CenterThreshold = 0.05f;

    private void Update()
    {
        UpdateTargetPosition();
        MoveToTargetPosition();
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

    public bool IsCenteredOnLane()
    {
        int lane = _laneSystem.LaneModel.CurrentLane;
        float targetX = _laneTransforms[lane].position.x;

        return Mathf.Abs(transform.position.x - targetX) < CenterThreshold;
    }
}
