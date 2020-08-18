using Cycler;
using UnityEngine;

namespace RPG.Inventory
{
    public interface InventoryControllerInterface
    {
        GameObject View { get; }
        int ChosenPosition { get; }
        ItemHolder ChosenItemHolder { get; }
        ICycler<ItemPossessor> CharCycler { get; }

        void ShowInventory();
        bool HasChosenEmptySlot();
        void DiscardItem(int amount);
    }
}
