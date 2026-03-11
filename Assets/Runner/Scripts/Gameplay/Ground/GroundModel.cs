
public class GroundModel
{
    public float CurrentSpeed {  get; private set; }

    private readonly float _startSpeed;
    private readonly float _accelerationSpeed;

    public GroundModel(float startSpeed, float speedAcceleration)
    {
        _startSpeed = startSpeed;
        _accelerationSpeed = speedAcceleration;

        CurrentSpeed = _startSpeed;
    }

    public void UpdateCurrentSpeed(float deltaTime)
    {
        CurrentSpeed += _accelerationSpeed * deltaTime;
    }
}
