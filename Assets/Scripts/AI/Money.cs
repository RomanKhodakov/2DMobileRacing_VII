using UnityEngine;

public sealed class Money : DataPlayer
{
    private int _countMoney;

    public Money(int baseMoney)
    {
        _countMoney = baseMoney;
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
            }
        }
    }
}