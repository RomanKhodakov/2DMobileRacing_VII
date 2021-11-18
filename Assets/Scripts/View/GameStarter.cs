using Profile;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private float _speedCar;
    [SerializeField] private UnityAdsTools _unityAdsTools;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _unityAdsTools);
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
