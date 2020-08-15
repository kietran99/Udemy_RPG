using Cycler;

namespace RPG.Inventory
{
    public interface InventoryControllerInterface
    {
        int ChosenPosition { get; }
        ICycler<ItemPossessor> CharCycler { get; }

        void ShowInventory();
    }
}
