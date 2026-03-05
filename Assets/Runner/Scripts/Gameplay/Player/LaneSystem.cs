using UnityEngine;

public class LaneSystem : MonoBehaviour 
{
    public LaneModel LaneModel { get; private set; }

    private void Awake()
    {
        LaneModel = new LaneModel();
    }

    public bool TryMove(int direction)
    {
        return LaneModel.SetCurrentLane(direction);
    }
}
