using System.Collections.Generic;

public class ItemsRepository : BaseController, IItemsRepository
{
    private readonly Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();
    public IReadOnlyDictionary<int, IItem> Items => _itemsMapById;

    public ItemsRepository(UpgradeItemConfigDataSource upgradeItemConfig)
    {
        PopulateItems(upgradeItemConfig);
    }

    private void PopulateItems(UpgradeItemConfigDataSource upgradeItemsConfigs)
    {
        foreach (var upgradeItemConfig in upgradeItemsConfigs.UpgradeItemConfigs)
        {
            if (_itemsMapById.ContainsKey(upgradeItemConfig.ItemConfig.Id))
                continue;

            _itemsMapById.Add(upgradeItemConfig.ItemConfig.Id, CreateItem(upgradeItemConfig));
        }
    }

    private IItem CreateItem(UpgradeItemConfig upgradeItemConfig)
    {
        return new Item 
        { 
            Id = upgradeItemConfig.ItemConfig.Id, 
            Info = new ItemInfo
            {
                Title = upgradeItemConfig.ItemConfig.Title,
                UpgradeType = upgradeItemConfig.UpgradeType,
                ValueUpgrade = upgradeItemConfig.ValueUpgrade,
                SpriteRenderer = upgradeItemConfig.ItemSpriteRenderer
            }
        };
    }
    
    protected override void OnDispose()
    {
        _itemsMapById.Clear();
    }
}
