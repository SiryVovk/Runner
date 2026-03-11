using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private ObstacleObjectPool _wallPool;
    [SerializeField] private ObstacleObjectPool _jumpPool;
    [SerializeField] private ObstacleObjectPool _slidePool;

    private Dictionary<EObstacleType, ObstacleObjectPool> _pools;

    private void Awake()
    {
        _pools = new Dictionary<EObstacleType, ObstacleObjectPool>
        {
            {EObstacleType.Wall, _wallPool},
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
