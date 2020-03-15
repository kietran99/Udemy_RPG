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
        userChooser.Activate(itemInteractor, itemsDisplay);
    }

    public void ToggleEquipAbility()
    {
        // Get the current possessor's inventory
        InventoryHolder invHolder = ItemManager.Instance.GetInvHolder(itemsDisplay.CurrentPossessor);

        // Equip/Unequip selected item to a CharStats
        ItemHolder holderToToggle = invHolder.ItemHolders[itemsDisplay.SelectedPos];
        Equipment itemToToggle = (Equipment) holderToToggle.TheItem;
        itemToToggle.ToggleEquipAbility(PossessorSearcher.GetPossessor(itemsDisplay.CurrentPossessor));

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

            // Find first empty slot in inventory then add the item to be equipped           
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
