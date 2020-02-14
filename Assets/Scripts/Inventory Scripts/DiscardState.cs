using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardState : InventoryState
{
    private ItemsDisplay itemsDisplay;

    public DiscardState(ItemsDisplay itemsDisplay)
    {
        this.itemsDisplay = itemsDisplay;
    }

    public void OnAmountConfirm(int changeAmount)
    {
        ItemManager.Instance.RemoveItemAt(itemsDisplay.CurrentPossessor, itemsDisplay.SelectedPos, changeAmount);
        itemsDisplay.DisplayAllItems();
    }

    public void OnItemSelected(int pos)
    {
        
    }
}
