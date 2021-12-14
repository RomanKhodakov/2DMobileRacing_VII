using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] 
    private Button _buttonStart; 
    
    [SerializeField] 
    private Button _buttonNotification;
    
    [SerializeField] 
    private Button _buttonRuLanguage;
    
    [SerializeField] 
    private Button _buttonEnLanguage;
        
    public void SubscribeAction(UnityAction startGame, UnityAction createNotification, Action<int> changeLanguage)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonNotification.onClick.AddListener(createNotification);
        _buttonRuLanguage.onClick.AddListener(() => changeLanguage(0));
        _buttonEnLanguage.onClick.AddListener(() => changeLanguage(1));
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonNotification.onClick.RemoveAllListeners();
    }
}

