using UnityEngine;
using UnityEngine.UI;

public class ItemMoveState : InventoryState
{
    private ItemsDisplay itemsDisplay;

    private GameObject itemInteractor, promptText;
    private int fromPos, toPos;
    private ItemOwner fromPossessor, toPossessor;
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

        if (currentInv[toPos].IsEmpty() || currentInv[toPos].CompareItem(currentInv[fromPos]))
        {
            itemsDisplay.EnableAmountSelector(itemQuantity);
            promptText.SetActive(false);
        }
        else promptText.GetComponent<Text>().text = "PLEASE CHOOSE A VALID SLOT!";
    }

    public void Activate(GameObject itemInteractor, GameObject promptText, ItemOwner fromPossessor, int fromPos, int itemQuantity)
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
