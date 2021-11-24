using System.Collections.Generic;

public interface IInventoryService
{
    IReadOnlyList<IItem> GetEquippedItems();
    void EquipItem(IItem item);
    void UnEquipItem(IItem item);
}
