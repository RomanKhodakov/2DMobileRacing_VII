using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUIView : MonoBehaviour
{
    [SerializeField] private Button[] _itemsButtons;
        
    public void SubscribeAction(List<UnityAction> onItemsClicks)
    {
        for (int i = 0; i < _itemsButtons.Length; i++)
        {
            if (onItemsClicks[i] != null)
            {
                _itemsButtons[i].onClick.AddListener(onItemsClicks[i]);
            }
        }
    }

    protected void OnDestroy()
    {
        foreach (var itemButton in _itemsButtons)
        {
            itemButton.onClick.RemoveAllListeners();
        }
    }
}


