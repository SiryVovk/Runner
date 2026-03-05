using UnityEngine;

public class GroundSegmentView : MonoBehaviour
{
    public float Length => length;

    [SerializeField] private float length = 28f;

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
}
