using Cycler;
using System;

namespace RPG.Inventory
{
    public interface InventoryControllerInterface
    {
        #region PROPERTIES
        InventoryViewInterface View { get; }
        int ChosenPosition { get; }
        ItemHolder ChosenItemHolder { get; }
        ICycler<InventoryOwner> CharCycler { get; }
        #endregion

        #region DELEGATES
        Action OnHide { get; set; }
        Action<bool, bool> OnUsableItemClick { get; set; }
        Action<DetailsData> OnItemMove { get; set; }
        #endregion

        #region METHODS
        void ShowInventory();
        bool HasChosenSameItemAt(int idx);
        bool HasChosenEmptySlot();
        bool IsEmptySlot(int idx);
        void Organize();
        void Discard(int amount);
        void MoveItem(int fromPos, int toPos, InventoryOwner sender, InventoryOwner receiver, int amount);
        void EquipItem(InventoryOwner charToEquip);
        void UnequipItem();
        #endregion
    }
}
