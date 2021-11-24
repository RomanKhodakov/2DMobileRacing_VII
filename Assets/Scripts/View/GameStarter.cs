using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private float _speedCar;
    [SerializeField] private UnityAdsTools _unityAdsTools;
    [SerializeField] private UpgradeItemConfigDataSource _upgradeItemConfigDataSource;
    [SerializeField] private AbilitiesDataSource _abilitiesDataSource;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _unityAdsTools);
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _upgradeItemConfigDataSource, _abilitiesDataSource);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
