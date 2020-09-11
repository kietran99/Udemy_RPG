using Cycler;
using UnityEngine;
using System;

namespace RPG.Inventory
{
    public interface InventoryControllerInterface
    {
        #region PROPERTIES
        GameObject View { get; }
        int ChosenPosition { get; }
        ItemHolder ChosenItemHolder { get; }
        ICycler<ItemOwner> CharCycler { get; }
        #endregion

        #region DELEGATES
        Action OnHide { get; set; }
        Action<bool, bool> OnUsableItemClick { get; set; }
        #endregion

        #region METHODS
        void ShowInventory();
        bool HasChosenSameItemAt(int idx);
        bool HasChosenEmptySlot();
        bool IsEmptySlot(int idx);
        void DiscardItem(int amount);
        void MoveItem(int fromPos, int toPos, ItemOwner sender, ItemOwner receiver, int amount);
        void EquipItem(CharStats charToEquip);
        void UnequipItem();
        #endregion
    }
}
