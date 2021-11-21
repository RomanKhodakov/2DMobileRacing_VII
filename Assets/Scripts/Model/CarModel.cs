public class CarModel
{
    private readonly float _defaultSpeed;
    public float Speed { get; private set; }

    public CarModel(float speed)
    {
        _defaultSpeed = speed;
        RestoreSpeed();
    }

    private void RestoreSpeed()
    {
        Speed = _defaultSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }
}

