using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Localization.Settings;
using Object = UnityEngine.Object;

public sealed class MainMenuController : BaseController
{
    private const string AndroidNotifierId = "android_notifier_id";
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _mainMenuView;

    public MainMenuController(Transform menuUiTransform, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _mainMenuView = LoadView(menuUiTransform);
        _mainMenuView.SubscribeAction(StartGame, CreateNotification, ChangeLanguage);
    }
    
    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
        _profilePlayer.AnalyticTools.SendMessage("Game_Started", ("Time", Time.realtimeSinceStartup));
        _profilePlayer.AdsShower.ShowInterstitial();
        Advertisement.AddListener(_profilePlayer.UnityAdsListener);
    }

    private void CreateNotification()
    {
        var androidSettingsChanel = new AndroidNotificationChannel
        {
            Id = AndroidNotifierId,
            Name = "Android Notifier",
            Importance =  Importance.High,
            CanShowBadge = true,
            Description = "Just being annoying",
            EnableLights = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };
      
        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChanel);
      
        var androidSettingsNotification = new AndroidNotification
        {
            Color = Color.white,
            RepeatInterval = TimeSpan.FromSeconds(5)
        };

        AndroidNotificationCenter.SendNotification(androidSettingsNotification, AndroidNotifierId);
        Debug.Log($"Notification created!");
    }

    private void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}

