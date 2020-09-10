using Cycler;
using UnityEngine;
using System;

namespace RPG.Inventory
{
    public interface InventoryControllerInterface
    {
        GameObject View { get; }
        int ChosenPosition { get; }
        ItemHolder ChosenItemHolder { get; }
        ICycler<ItemOwner> CharCycler { get; }

        Action OnHide { get; set; }
        Action<bool> OnUsableItemClick { get; set; }

        void ShowInventory();
        bool HasChosenSameItemAt(int idx);
        bool HasChosenEmptySlot();
        bool IsEmptySlot(int idx);
        void DiscardItem(int amount);
        void MoveItem(int fromPos, int toPos, ItemOwner sender, ItemOwner receiver, int amount);
        void EquipItem(CharStats charToEquip);
    }
}
