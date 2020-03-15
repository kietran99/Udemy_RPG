using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : InventoryState
{
    private ItemsDisplay itemsDisplay;

    public DefaultState(ItemsDisplay itemsDisplay)
    {
        this.itemsDisplay = itemsDisplay;
    }

    public void OnAmountConfirm(int changeAmount)
    {
        
    }

    public void OnItemSelected(int pos)
    {
        // Display item details  
        ItemHolder selectedHolder = itemsDisplay.CurrentInv[pos];
        Item selectedItem = selectedHolder.TheItem;
        selectedItem.SetPrimaryAction(selectedHolder.IsEquipped);
        
        itemsDisplay.ItemNameText.text = selectedItem.ItemName;
        itemsDisplay.ItemDescriptionText.text = selectedItem.Description;
        itemsDisplay.PrimaryActionText.text = selectedItem.GetPrimaryAction();
    }
}
