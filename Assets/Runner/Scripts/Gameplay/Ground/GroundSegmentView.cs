using UnityEngine;

public class GroundSegmentView : MonoBehaviour
{
    public float Length { get; private set; }

    [SerializeField] private Renderer _groundRenderer;

    private void Awake()
    {
        Length = _groundRenderer.bounds.size.z;
    }

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
