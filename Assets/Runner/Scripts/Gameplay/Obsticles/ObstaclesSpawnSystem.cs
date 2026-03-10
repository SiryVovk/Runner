using UnityEngine;

public class ObstaclesSpawnSystem : MonoBehaviour
{
    [SerializeField] private ObstacleFactory _obstacleFactory;


    private const int MaxLanes = 2;

    public void SpawnObsticles(GroundSegmentView segmentView)
    {
        ClearObsticles(segmentView);

        foreach (var rows in segmentView.Rows)
        {
            Transform[] spawnPoinsInRow = rows.GetAll();
            CreatObjects(segmentView, spawnPoinsInRow);

        }
    }

    private void CreatObjects(GroundSegmentView segmentView, Transform[] spawnPoints)
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
        EObstacleType type = (EObstacleType)Random.Range(0, System.Enum.GetValues(typeof(EObstacleType)).Length);

        if (type == EObstacleType.Wall )
        {
            if (laneCount >= MaxLanes)
            {
                do
                {
                    type = (EObstacleType)Random.Range(0, System.Enum.GetValues(typeof(EObstacleType)).Length);
                }while(type == EObstacleType.Wall);
            }
            else
            {
                laneCount++;
            }
        }

        return type;
    }

    public void ClearObsticles(GroundSegmentView segmentView)
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
