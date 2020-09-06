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
        ICycler<ItemPossessor> CharCycler { get; }

        Action OnHide { get; set; }
        Action<bool> OnUsableItemClick { get; set; }

        void ShowInventory();
        bool HasChosenSameItemAt(int idx);
        bool HasChosenEmptySlot();
        bool IsEmptySlot(int idx);
        void DiscardItem(int amount);
        void MoveItem(int fromPos, ItemPossessor sender, int amount);
    }
}
