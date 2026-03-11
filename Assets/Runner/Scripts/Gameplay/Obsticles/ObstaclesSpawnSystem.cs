using UnityEngine;

public class ObstaclesSpawnSystem : MonoBehaviour
{
    [SerializeField] private ObstacleFactory _obstacleFactory;


    private const int MaxLanes = 2;
    private readonly int _obstacleTypesCount = System.Enum.GetValues(typeof(EObstacleType)).Length;

    public void SpawnObstacles(GroundSegmentView segmentView)
    {
        ClearObstacles(segmentView);

        foreach (var row in segmentView.Rows)
        {
            Transform[] spawnPointsInRow = row.GetAll();
            CreateObjects(segmentView, spawnPointsInRow);

        }
    }

    private void CreateObjects(GroundSegmentView segmentView, Transform[] spawnPoints)
    {
        int laneCount = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            EObstacleType type = GetRandomType(ref laneCount);


            if(type == EObstacleType.None)
            {
                continue;
            }

            ObstacleHolder obstacle = _obstacleFactory.CreateObstacle(type);

            obstacle.transform.SetParent(spawnPoints[i]);
            obstacle.transform.localPosition = Vector3.zero;

            segmentView.AddToList(obstacle);
        }
    }

    private EObstacleType GetRandomType(ref int laneCount)
    {
        EObstacleType type = (EObstacleType)Random.Range(0, _obstacleTypesCount);

        if (type == EObstacleType.Wall )
        {
            if (laneCount >= MaxLanes)
            {
                do
                {
                    type = (EObstacleType)Random.Range(0, _obstacleTypesCount);
                }while(type == EObstacleType.Wall);
            }
            else
            {
                laneCount++;
            }
        }

        return type;
    }

    public void ClearObstacles(GroundSegmentView segmentView)
    {
        foreach(var obstacle in segmentView.Obstacles)
        {
            if (obstacle.ObstacleType != EObstacleType.None)
            {
                _obstacleFactory.ReleaseObstacle(obstacle);
            }
        }

        segmentView.ClearList();
    }
}
