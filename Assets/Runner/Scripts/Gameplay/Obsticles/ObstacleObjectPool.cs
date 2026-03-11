using UnityEngine;
using UnityEngine.Pool;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField] private ObstacleHolder _obstaclePrefab;
    [SerializeField] private int _poolSize = 15;
    [SerializeField] private int _maxPoolSize = 30;

    private ObjectPool<ObstacleHolder> _obstacleObjectPool;

    private void Awake()
    {
        _obstacleObjectPool = new ObjectPool<ObstacleHolder>(
                () => CreateObstacle(_obstaclePrefab),
                OnGetObstacle,
                OnReleaseObstacle,
                OnDestroyObstacle,
                true,
                _poolSize,
                _maxPoolSize
            );
    }

    private ObstacleHolder CreateObstacle(ObstacleHolder prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void OnGetObstacle(ObstacleHolder obstacle)
    {
        obstacle.gameObject.SetActive(true);
    }

    private void OnReleaseObstacle(ObstacleHolder obstacle)
    {
        obstacle.transform.parent = transform;
        obstacle.gameObject.SetActive(false);
    }

    private void OnDestroyObstacle(ObstacleHolder obstacle)
    {
        if(obstacle == null)
        {
            return;
        }

        Destroy(obstacle.gameObject);
    }

    public ObstacleHolder GetObstacle()
    {
        return _obstacleObjectPool.Get();
    }

    public void ReleaseObstacle(ObstacleHolder obstacle)
    {
        _obstacleObjectPool.Release(obstacle);
    }
}
