using UnityEngine;

public sealed class Health : DataPlayer
{
    private int _countHealth;

    public Health(int baseHealth)
    {
        CountHealth = baseHealth;
    }
    public int CountHealth
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                _countHealth = value;
                Notifier(DataType.Health);
                Debug.Log($"Notified Health");
            }
        }
    }
}