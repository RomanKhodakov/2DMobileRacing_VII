using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFightWindowView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countMoneyText;
    [SerializeField] private TMP_Text _countHealthText;
    
    [SerializeField] private TMP_Text _countPowerText;
    [SerializeField] private TMP_Text _countPowerEnemyText;

    [SerializeField] private Button _addCoinsButton;
    [SerializeField] private Button _minusCoinsButton;
    
    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;

    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;

    [SerializeField] private Button _fightButton;
    [SerializeField] private Button _skipButton;

    public TMP_Text CountMoneyText => _countMoneyText;
    public TMP_Text CountHealthText => _countHealthText;
    public TMP_Text CountPowerText => _countPowerText;
    public TMP_Text CountPowerEnemyText => _countPowerEnemyText;
    
    public Button SkipButton => _skipButton;

    public void SubscribeActions(List<UnityAction> onClickChanges)
    {

        _addCoinsButton.onClick.AddListener(onClickChanges[0]);
        _minusCoinsButton.onClick.AddListener(onClickChanges[1]);

        _addHealthButton.onClick.AddListener(onClickChanges[2]);
        _minusHealthButton.onClick.AddListener(onClickChanges[3]);

        _addPowerButton.onClick.AddListener(onClickChanges[4]);
        _minusPowerButton.onClick.AddListener(onClickChanges[5]);

        _fightButton.onClick.AddListener(onClickChanges[6]);
        _skipButton.onClick.AddListener(onClickChanges[7]);
    }

    protected void OnDestroy()
    {
        _addCoinsButton.onClick.RemoveAllListeners();
        _minusCoinsButton.onClick.RemoveAllListeners();

        _addCoinsButton.onClick.RemoveAllListeners();
        _minusCoinsButton.onClick.RemoveAllListeners();

        _addCoinsButton.onClick.RemoveAllListeners();
        _minusCoinsButton.onClick.RemoveAllListeners();
        
        _fightButton.onClick.RemoveAllListeners();
        _skipButton.onClick.RemoveAllListeners();
    }
}
