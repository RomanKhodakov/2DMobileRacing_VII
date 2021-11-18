using Profile;
using UnityEngine;
using UnityEngine.Advertisements;

public sealed class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _mainMenuView;

    public MainMenuController(Transform menuUiTransform, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _mainMenuView = LoadView(menuUiTransform);
        _mainMenuView.SubscribeAction(StartGame);
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
}

