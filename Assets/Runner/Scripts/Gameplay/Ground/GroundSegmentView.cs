using System.Collections.Generic;
using UnityEngine;

public class GroundSegmentView : MonoBehaviour
{
    public float Length => _length;
    public List<ObstacleHolder> Obstacles => _obstacles;
    public SpawnRowStruct[] Rows => _spawnPoints;

    [SerializeField] private float _length = 28f;
    [SerializeField] private SpawnRowStruct[] _spawnPoints;

    private List<ObstacleHolder> _obstacles = new List<ObstacleHolder>();

    public void Move(float deltaZMove)
    {
        Vector3 newPosition = transform.position;
        newPosition.z += deltaZMove;
        transform.position = newPosition;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void AddToList(ObstacleHolder obstacleHolder)
    {
        _obstacles.Add(obstacleHolder);
    }

    public void ClearList()
    {
        _obstacles.Clear();
    }
}
