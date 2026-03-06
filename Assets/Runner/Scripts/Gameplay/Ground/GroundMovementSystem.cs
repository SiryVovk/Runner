using System.Collections.Generic;
using UnityEngine;

public class GroundMovementSystem : MonoBehaviour
{
    [SerializeField] private List<GroundSegmentView> _groundSegments;
    [SerializeField] private GroundSpeedSystem _groundSpeedSystem;
    [SerializeField] private ObstaclesSpawnSystem _obstaclesSpawnSystem;
    [SerializeField] private float zTeleportPosition = -15;

    private const int DirectionMultiplyer = -1;

    private void Update()
    {
        float speed = _groundSpeedSystem.GroundModel.CurrentSpeed;
        float deltaZPosition = speed * DirectionMultiplyer * Time.deltaTime;

        MoveSegments(deltaZPosition);
        UpdateGroundPosition();
    }

    private void MoveSegments(float deltaZMove)
    {
        foreach (GroundSegmentView segment in _groundSegments)
        {
            segment.Move(deltaZMove);
        }
    }

    private void UpdateGroundPosition()
    {
        GroundSegmentView firstGroundSegment = _groundSegments[0];

        if(firstGroundSegment.transform.position.z < zTeleportPosition)
        {
            GroundSegmentView lastSegment = _groundSegments[_groundSegments.Count - 1];

            Vector3 newPosition = new Vector3(0f,0f,lastSegment.transform.position.z + lastSegment.Length);
            firstGroundSegment.SetPosition(newPosition);

            _obstaclesSpawnSystem.SpawnObsticles(firstGroundSegment);
            _groundSegments.RemoveAt(0);
            _groundSegments.Add(firstGroundSegment);
        }
    }
}
