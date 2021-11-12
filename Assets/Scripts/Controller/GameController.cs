using Tools;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer)
    {
        var leftMoveProperty = new SubscriptionProperty<float>();
        var rightMoveProperty = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveProperty, rightMoveProperty);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveProperty, rightMoveProperty, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);
    }
}

