using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMoveState : InventoryState
{
    private ItemsDisplay itemsDisplay;

    private GameObject itemInteractor, promptText;
    private int fromPos, toPos;
    private PossessorSearcher.ItemPossessor fromPossessor, toPossessor;
    private int itemQuantity;

    public ItemMoveState(ItemsDisplay itemsDisplay)
    {
        this.itemsDisplay = itemsDisplay;
    }

    public void OnAmountConfirm(int changeAmount)
    {       
        ItemManager.Instance.GetInvHolder(fromPossessor).MoveItem(fromPos, toPos, changeAmount, ItemManager.Instance.GetInvHolder(toPossessor));
        itemsDisplay.ToDefaultState();
    }  

    public void OnItemSelected(int toPos)
    {
        this.toPos = toPos;
        toPossessor = itemsDisplay.CurrentPossessor;
        ItemHolder[] currentInv = ItemManager.Instance.GetInventory(toPossessor);

        if (currentInv[toPos].IsEmpty() || currentInv[toPos].IdenticalItem(currentInv[fromPos]))
        {
            itemsDisplay.EnableAmountSelector(itemQuantity);
            promptText.SetActive(false);
        }
        else promptText.GetComponent<Text>().text = "PLEASE CHOOSE A VALID SLOT!";
    }

    public void Activate(GameObject itemInteractor, GameObject promptText, PossessorSearcher.ItemPossessor fromPossessor, int fromPos, int itemQuantity)
    {
        this.fromPossessor = fromPossessor;
        this.fromPos = fromPos;
        this.itemQuantity = itemQuantity;
        this.itemInteractor = itemInteractor;
        this.promptText = promptText;
        this.itemInteractor.SetActive(false);
        this.promptText.SetActive(true);
    }
}
