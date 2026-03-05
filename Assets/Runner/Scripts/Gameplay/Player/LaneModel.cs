using UnityEngine;

public class LaneModel
{
    public int CurrentLane { get; private set;} = 1;

    private const int MinLane = 0;
    private const int MaxLane = 2;

    public bool SetCurrentLane(int direction)
    {
        int targetLane = CurrentLane + direction;

        if(targetLane < MinLane || targetLane > MaxLane)
        {
            return false;
        }

        CurrentLane = targetLane;

        return true;
    }
}
