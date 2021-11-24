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
    private readonly List<UnityAction> _onClickActions;

    public Action HideAction { get; }

    public InventoryController(UpgradeItemConfigDataSource upgradeItemConfigDataSource, IEnumerable<AbilityItemConfig> abilitiesItemConfigs,
        Transform menuUiTransform, CarModel carModel, CarView carView)
    {
        var itemsRepository = new ItemsRepository(upgradeItemConfigDataSource);
        var abilityRepository = new AbilityRepository(abilitiesItemConfigs);
        _inventoryService = new InventoryService(itemsRepository.Items, abilityRepository.Collection, carModel, carView);

        _onClickActions = _inventoryService.GetOnClickButtonsActions();
        
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
