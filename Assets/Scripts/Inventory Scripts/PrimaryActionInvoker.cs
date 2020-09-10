using RPG.Inventory;
using UnityEngine;

public class PrimaryActionInvoker
{
    private ItemsDisplay itemsDisplay;

    public PrimaryActionInvoker(ItemsDisplay itemsDisplay)
    {
        this.itemsDisplay = itemsDisplay;
    }

    public void UseItem(GameObject itemInteractor, UserChooser userChooser)
    {
        userChooser.Activate();
    }

    public void ToggleEquipAbility()
    {
        InventoryHolder invHolder = ItemManager.Instance.GetInvHolder(itemsDisplay.CurrentPossessor);
        Equipment itemToToggle = (Equipment) ItemManager.Instance.GetItemAt(itemsDisplay.SelectedPos, itemsDisplay.CurrentPossessor);
        itemToToggle.ToggleEquipAbility(PossessorSearcher.GetStats(itemsDisplay.CurrentPossessor));

        if (invHolder.ItemHolders[itemsDisplay.SelectedPos].IsEquipped)
        {
            OrganizeAfterToggle(invHolder, itemsDisplay.SelectedPos, false);
        }
        else
        {
            int equippedItemPos = invHolder.FindSameEquippedType(itemToToggle); 

            if (equippedItemPos != InventoryHolder.POSITION_INVALID)
            {                
                OrganizeAfterToggle(invHolder, equippedItemPos, false);
            }
    
            OrganizeAfterToggle(invHolder, itemsDisplay.SelectedPos, true);
        }

        itemsDisplay.DisplayAll();
    }

    private void OrganizeAfterToggle(InventoryHolder invHolder, int pos, bool equip)
    {
        int newPos = invHolder.FindFirstEmptySlot();
        invHolder.MoveItem(pos, newPos, 1);
        invHolder.ItemHolders[newPos].IsEquipped = equip;
    }
}
