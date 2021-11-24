using System.Collections.Generic;
using UnityEngine.Events;

public sealed class InventoryService : IInventoryService
{
    private readonly List<IItem> _equippedItems = new List<IItem>();

    private readonly List<UnityAction> _onClickActions = new List<UnityAction>();

    private readonly IReadOnlyDictionary<int, IItem> _items;
    private readonly IReadOnlyDictionary<int, IAbility> _abilities;
    private readonly CarModel _carModel;
    private readonly CarView _carView;

    public InventoryService(IReadOnlyDictionary<int, IItem> items, IReadOnlyDictionary<int, IAbility> abilities,
        CarModel carModel, CarView carView)
    {
        _items = items;
        _abilities = abilities;
        _carModel = carModel;
        _carView = carView;
    }

    public List<UnityAction> GetOnClickButtonsActions()
    {
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

        foreach (var ability in _abilities.Values)
        {
            switch (ability.Config.AbilityType)
            {
                case AbilityType.Bomb:
                    _onClickActions.Add(() =>
                    {
                        ability.Apply();
                    });
                    break;
                case AbilityType.Gun:
                    break;
                default:
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