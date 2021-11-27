using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly UpgradeItemConfigDataSource _upgradeItemConfigDataSource;
    private readonly AbilitiesDataSource _abilitiesDataSource;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, 
        UpgradeItemConfigDataSource upgradeItemConfigDataSource, AbilitiesDataSource abilitiesDataSource)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _upgradeItemConfigDataSource = upgradeItemConfigDataSource;
        _abilitiesDataSource = abilitiesDataSource;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }
    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _placeForUi, _upgradeItemConfigDataSource, _abilitiesDataSource);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

}
