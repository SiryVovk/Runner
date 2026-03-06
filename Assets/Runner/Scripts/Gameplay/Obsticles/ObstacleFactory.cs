using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private ObstacleObjectPoll _lanePool;
    [SerializeField] private ObstacleObjectPoll _jumpPool;
    [SerializeField] private ObstacleObjectPoll _slidePool;

    private Dictionary<EObstacleType, ObstacleObjectPoll> _pools;

    private void Awake()
    {
        _pools = new Dictionary<EObstacleType, ObstacleObjectPoll>
        {
            {EObstacleType.Lane, _lanePool},
            {EObstacleType.Jump, _jumpPool},
            {EObstacleType.Slide, _slidePool}
        };
    }

    public ObstacleHolder CreateObstacle(EObstacleType obstacleType)
    {
        return _pools[obstacleType].GetObstacle();
    }

    public void ReleaseObstacle(ObstacleHolder obstacle)
    {
        _pools[obstacle.ObstacleType].ReleaseObstacle(obstacle);
    }
}
