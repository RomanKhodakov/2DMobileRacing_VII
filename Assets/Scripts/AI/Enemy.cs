using Random = UnityEngine.Random;

public sealed class Enemy : IEnemy
{
    private readonly string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    
    private int _newBaseEnemyPower;
    public int Power => _moneyPlayer + _healthPlayer - _powerPlayer + _newBaseEnemyPower;
    
    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                var dataMoney = (Money)dataPlayer;
                 _moneyPlayer = dataMoney.CountMoney;
                break;

            case DataType.Health:
                var dataHealth = (Health)dataPlayer;
                _healthPlayer = dataHealth.CountHealth;
                break;

            case DataType.Power:
                var dataPower = (Power)dataPlayer;
                _powerPlayer = dataPower.CountPower;
                break;
        }
    }

    public void GetNewEnemy()
    {
        _newBaseEnemyPower = Random.Range(-1, 3);
    }
}
