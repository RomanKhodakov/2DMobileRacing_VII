using System.Collections.Generic;
using UnityEngine.Events;

public sealed class InventoryService : IInventoryService
{
    private readonly List<IItem> _equippedItems = new List<IItem>();
    
    private readonly List<UnityAction> _onClickActions = new List<UnityAction>();

    private readonly IReadOnlyDictionary<int, IItem> _items;
    private readonly CarModel _carModel;
    private readonly CarView _carView;

    public InventoryService(IReadOnlyDictionary<int, IItem> items, CarModel carModel, CarView carView)
    {
        _items = items;
        _carModel = carModel;
        _carView = carView;
    }

    public List<UnityAction> GetOnClickButtonsItemsActions()
    { // тут через замыкание идёт захват текущего айтема для добавления в список с событиями
        foreach (var item in _items.Values)
        {
            switch (item.Info.UpgradeType)
            {
                case UpgradeType.Speed:
                    _onClickActions.Add(() =>
                    {
                        _carModel.SetSpeed(item.Info.ValueUpgrade);
                        var color = item.Info.SpriteRenderer.color;
                        _carView.WheelsSpritesRenderers[0].color = color;
                        _carView.WheelsSpritesRenderers[1].color = color;
                    });
                    break;
                case UpgradeType.Visual:
                    _onClickActions.Add(() =>
                    {
                        _carView.CarFrameSpriteRenderer.color = item.Info.SpriteRenderer.color;
                    });
                    break;
                case UpgradeType.None:
                    break;
            }
        }
        return _onClickActions;
    }

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _equippedItems;
    }

    public void EquipItem(IItem item)
    {
        if (_equippedItems.Contains(item))
            return;

        _equippedItems.Add(item);
    }

    public void UnEquipItem(IItem item)
    {
        if (!_equippedItems.Contains(item))
            return;

        _equippedItems.Remove(item);
    }
}
