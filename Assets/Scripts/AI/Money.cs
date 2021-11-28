using UnityEngine;

public sealed class Money : DataPlayer
{
    private int _countMoney;

    public Money(int baseMoney)
    {
        CountMoney = baseMoney;
    }

    public int CountMoney
    {
        get => _countMoney;
        set
        {
            if (_countMoney != value)
            {
                _countMoney = value;
                Notifier(DataType.Money);   
                Debug.Log($"Notified Money");
            }
        }
    }
}