using System;
using System.Collections.Generic;
using UnityEngine.Events;

public interface IInventoryView
{
    public void SubscribeActionsOnButtons(List<UnityAction> onItemsClicks);
    public void UnSubscribeActionsOnButtons();
    public void Display(IReadOnlyList<IItem> items);
    public void Hide();
}
