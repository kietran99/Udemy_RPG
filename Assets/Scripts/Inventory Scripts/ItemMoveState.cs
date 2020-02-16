using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveState : InventoryState
{
    private ItemsDisplay itemsDisplay;

    private int fromPos, toPos;
    private PossessorSearcher.ItemPossessor fromPossessor, toPossessor;
    private int itemQuantity;

    public ItemMoveState(ItemsDisplay itemsDisplay)
    {
        this.itemsDisplay = itemsDisplay;
    }

    public void OnAmountConfirm(int changeAmount)
    {       
        Item temp = ItemManager.Instance.GetItemAt(fromPos, fromPossessor);
        ItemManager.Instance.RemoveItemAt(fromPossessor, fromPos, changeAmount);
        ItemManager.Instance.AddItemAt(toPossessor, new ItemHolder(temp, changeAmount), toPos, changeAmount);
    }  

    public void OnItemSelected(int toPos)
    {
        this.toPos = toPos;
        toPossessor = itemsDisplay.CurrentPossessor;
        itemsDisplay.EnableAmountSelector(itemQuantity);       
    }

    public void SetItemToBeMoved(PossessorSearcher.ItemPossessor fromPossessor, int fromPos, int itemQuantity)
    {
        this.fromPossessor = fromPossessor;
        this.fromPos = fromPos;
        this.itemQuantity = itemQuantity;
    }
}
