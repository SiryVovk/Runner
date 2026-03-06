using UnityEngine;

public class ObstacleHolder : MonoBehaviour, IObstacle
{
    public EObstacleType ObstacleType => _obstacleType;

    [SerializeField] private EObstacleType _obstacleType;

}
