using Profile;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private float _speedCar;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_speedCar);
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
