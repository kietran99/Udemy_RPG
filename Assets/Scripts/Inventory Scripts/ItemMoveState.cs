using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveState : InventoryState
{
    private ItemsDisplay itemsDisplay;

    private int fromPos, toPos;
    private PossessorSearcher.ItemPossessor fromPossessor, toPossessor;
    private ItemHolder itemToMove;

    public ItemMoveState(ItemsDisplay itemsDisplay)
    {
        this.itemsDisplay = itemsDisplay;
    }

    public void OnAmountConfirm(int changeAmount)
    {       
        Item temp = ItemManager.Instance.GetItemAt(fromPos, fromPossessor);
        itemToMove = new ItemHolder(temp, changeAmount, fromPossessor);
        ItemManager.Instance.RemoveItemAt(fromPossessor, fromPos, changeAmount);
        ItemManager.Instance.AddItemAt(toPossessor, itemToMove, toPos, changeAmount);
        itemsDisplay.DisplayAllItems();
    }  

    public void OnItemSelected(int toPos)
    {
        this.toPos = toPos;
        this.toPossessor = itemsDisplay.CurrentPossessor;
        itemsDisplay.EnableAmountSelector();       
    }

    public void SetItemToBeMoved(PossessorSearcher.ItemPossessor fromPossessor, int fromPos)
    {
        this.fromPossessor = fromPossessor;
        this.fromPos = fromPos;
    }
}
