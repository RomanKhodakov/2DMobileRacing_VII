using Tools;
using UnityEngine;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, Transform placeForUi, 
        UpgradeItemConfigDataSource upgradeItemConfigDataSource, AbilitiesDataSource abilitiesDataSource)
    {
        var leftMoveProperty = new SubscriptionProperty<float>();
        var rightMoveProperty = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveProperty, rightMoveProperty);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveProperty, rightMoveProperty, profilePlayer.CurrentCarModel);
        AddController(inputGameController);
        
        
        var carController = new CarController();
        AddController(carController);
        
        var inventoryController = new InventoryController(upgradeItemConfigDataSource, abilitiesDataSource.AbilitiesItemsConfigs, placeForUi,
            profilePlayer.CurrentCarModel, carController.GetCarView());
        AddController(inventoryController);
    }
}

