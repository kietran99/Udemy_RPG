using UnityEngine;
using RPG.Inventory;
using System;

public class ItemSaleDisplay : MonoBehaviour, InventoryViewInterface
{
    [SerializeField]
    private GameObject invSlotPrototype = null, invOrganizer = null;

    private ItemButton[] itemButtons;

    public Func<int, DetailData> OnItemButtonClick { get; set; }

    public void Init(SellMenuController controller, ItemHolder[] itemsToDisplay)
    {
        if (itemButtons != null) return;

        itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

        for (int i = 0; i < itemButtons.Length; i++)
        {
            GameObject btn = Instantiate(invSlotPrototype);
            itemButtons[i] = btn.GetComponent<ItemButton>();
            btn.transform.SetParent(invOrganizer.transform);

            itemButtons[i].Init(controller, i);
            itemButtons[i].DisplayItem(itemsToDisplay[i]);
        }
    }

    public void Display(ItemHolder[] holders)
    {      
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].DisplayItem(holders[i]);
        }
    }

    public void UpdateSlot(int slot, ItemHolder holder)
    {
        itemButtons[slot].DisplayItem(holder);
    }
}
