using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class InventoryController : BaseController, IInventoryController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/InventoryUI"};
    private readonly InventoryService _inventoryService;
    private readonly InventoryView _inventoryView;
    private readonly ItemsRepository _itemsRepository;
    private readonly List<UnityAction> _onClickActions;

    public Action HideAction { get; }

    public InventoryController(UpgradeItemConfigDataSource upgradeItemConfigDataSource, Transform menuUiTransform, 
        CarModel carModel, CarView carView)
    {
        _itemsRepository = new ItemsRepository(upgradeItemConfigDataSource);
        _inventoryService = new InventoryService(_itemsRepository.Items, carModel, carView);

        _onClickActions = _inventoryService.GetOnClickButtonsItemsActions();
        
        _inventoryView = LoadView(menuUiTransform);

        SubscribeView();
    }

    private InventoryView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<InventoryView>();
    }

    public void ShowInventory()
    {
        _inventoryView.Display(_inventoryService.GetEquippedItems());
    }

    public void HideInventory()
    {
        _inventoryView.Hide();
        HideAction?.Invoke();
    }

    private void SubscribeView()
    {
        _inventoryView.SubscribeActionsOnButtons(_onClickActions);
    }

    private void OnItemSelected(IItem item)
    {
        _inventoryService.EquipItem(item);
    }

    private void OnItemDeselected(IItem item)
    {
        _inventoryService.UnEquipItem(item);
    }

    protected override void OnDispose()
    {
        _inventoryView.UnSubscribeActionsOnButtons();

        base.OnDispose();
    }
}
