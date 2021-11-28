using Tools;
using UnityEngine;
using UnityEngine.Advertisements;

public sealed class ProfilePlayer
{
    private const string MoneyKey = nameof(MoneyKey);
    private const string HealthKey = nameof(HealthKey);
    public ProfilePlayer(float speedCar, UnityAdsTools unityAdsTools)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCarModel = new CarModel(speedCar);
        AnalyticTools = new UnityAnalyticTools();
        AdsShower = unityAdsTools;
        UnityAdsListener = unityAdsTools;
    }
    public int Money
    {
        get => PlayerPrefs.GetInt(MoneyKey, 0);
        set => PlayerPrefs.SetInt(MoneyKey, value);
    }
    
    public int Health
    {
        get => PlayerPrefs.GetInt(HealthKey, 0);
        set => PlayerPrefs.SetInt(HealthKey, value);
    }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public CarModel CurrentCarModel { get; }
    
    public IAnalyticTools AnalyticTools { get; }

    public IAdsShower AdsShower { get; }
    public IUnityAdsListener UnityAdsListener { get; }
}

