using UnityEngine;

public class GroundSpeedSystem : MonoBehaviour
{
    public GroundModel GroundModel {  get; private set; }

    [SerializeField] private float _startSpeed;
    [SerializeField] private float _accelerationSpeed;

    private void Awake()
    {
        GroundModel = new GroundModel(_startSpeed, _accelerationSpeed);
    }

    private void Update()
    {
        GroundModel.UpdateCurrentSpeed(Time.deltaTime);
    }
}
