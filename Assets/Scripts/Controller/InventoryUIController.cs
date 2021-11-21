using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public sealed class InventoryUIController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/InventoryUI"};
    
    private readonly List<UnityAction> _onClickActions = new List<UnityAction>();

    public InventoryUIController(Transform menuUiTransform)
    {
        var inventoryUIView = LoadView(menuUiTransform);
        inventoryUIView.SubscribeAction(_onClickActions);
    }
    
    private InventoryUIView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<InventoryUIView>();
    }

    private void StartGame()
    {
        
    }
}

