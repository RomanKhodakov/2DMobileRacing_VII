using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public sealed class UIFightController : BaseController
{
    private const int EnemyPowerThreshold = 3;
    private const float SkipButtonMoveTime = 0.5f;
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/FightUI"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly UIFightWindowView _uiFightWindowView;

    private readonly Money _money;
    private readonly Health _health;
    private readonly Power _power;
    
    private readonly Enemy _enemy;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;

    private readonly List<UnityAction> _onClickChanges;

    private Vector2 _basePosition;

    public UIFightController(Transform menuUiTransform, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        
        _enemy = new Enemy("Enemy");

        _money = new Money(profilePlayer.Money);
        _money.Attach(_enemy);

        _health = new Health(profilePlayer.Health);
        _health.Attach(_enemy);

        _power = new Power();
        _power.Attach(_enemy);
        
        _onClickChanges = new List<UnityAction>();
        
        _uiFightWindowView = LoadView(menuUiTransform);
        _uiFightWindowView.SubscribeActions(GetOnClickActions());
        
        SetBasePlayerStats();
    }
    
    private UIFightWindowView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<UIFightWindowView>();
    }

    private List<UnityAction> GetOnClickActions()
    {
        _onClickChanges.Add(() => ChangeMoney(true));
        _onClickChanges.Add(() => ChangeMoney(false));
        
        _onClickChanges.Add(() => ChangeHealth(true));
        _onClickChanges.Add(() => ChangeHealth(false));
        
        _onClickChanges.Add(() => ChangePower(true));
        _onClickChanges.Add(() => ChangePower(false));
        
        _onClickChanges.Add(Fight);
        _onClickChanges.Add(Skip);
        
        return _onClickChanges;
    }

    private void SetBasePlayerStats()
    {
        _power.CountPower = _profilePlayer.Money + _profilePlayer.Health;
        _allCountMoneyPlayer = _profilePlayer.Money;
        _allCountHealthPlayer = _profilePlayer.Health;
        _allCountPowerPlayer = _power.CountPower;
        
        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }


    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }
    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _uiFightWindowView.CountMoneyText.text = $"Player Money: {countChangeData}";
                _money.CountMoney = countChangeData;
                break;

            case DataType.Health:
                _uiFightWindowView.CountHealthText.text = $"Player Health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;

            case DataType.Power:
                _uiFightWindowView.CountPowerText.text = $"Player Power: {countChangeData}";
                _power.CountPower = countChangeData;
                break;
        }

        UpdateEnemyPowerText();
    }

    private void UpdateEnemyPowerText()
    {
        _uiFightWindowView.CountPowerEnemyText.text = $"Power Enemy: {_enemy.Power}";
    }

    private void Fight()
    {
        if (_allCountPowerPlayer > _enemy.Power)
        {
            Debug.Log("Win");
            _enemy.GetNewEnemy();
            UpdateEnemyPowerText();
            _uiFightWindowView.SkipButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Lose");
        }
    }

    private void Skip()
    {
        if (_enemy.Power >= EnemyPowerThreshold)
        {
            _basePosition = _uiFightWindowView.SkipButton.transform.position;
            _uiFightWindowView.SkipButton.transform.DOMove(_basePosition * Vector2.right, SkipButtonMoveTime)
                .SetEase(Ease.InOutSine).onComplete += () =>
                {
                    _uiFightWindowView.SkipButton.gameObject.SetActive(false);
                    _uiFightWindowView.SkipButton.transform.position = _basePosition;
                };
            
            Debug.Log($"Can't skip powerful enemy");
        }
        else
        {
            _enemy.GetNewEnemy();
            UpdateEnemyPowerText();
        }
    }

    protected override void OnDispose()
    {
        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        
        base.OnDispose();
    }
}


