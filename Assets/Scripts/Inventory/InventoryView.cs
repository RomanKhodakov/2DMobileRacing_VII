using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IInventoryView
{
    [SerializeField] private Button[] _itemsButtons;

    public void SubscribeActionsOnButtons(List<UnityAction> onItemsClicks)
    {
        for (int i = 0; i < _itemsButtons.Length && i < onItemsClicks.Count; i++)
        {
            _itemsButtons[i].onClick.AddListener(onItemsClicks[i]);
        }
    }

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }

    public void Hide()
    {
        Debug.Log($"Close Inventory");
    }

    public void UnSubscribeActionsOnButtons()
    {
        for (int i = 0; i < _itemsButtons.Length; i++)
        {
            _itemsButtons[i].onClick.RemoveAllListeners();
        }
    }
}