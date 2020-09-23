namespace RPG.Inventory
{
    public interface InventoryHolderInterface
    {
        ItemOwner Possessor { get; set; }
        ItemHolder[] ItemHolders { get; set; }

        int FindFirstEmptySlot();
        void RemoveAt(int posToRemove, int amount);
        int Add(ItemHolder itemHolderToAdd);
        void AddAt(ItemHolder itemHolderToAdd, int posToAdd);
        int FindSameEquippedTypePos(int posToCompare);
        void MoveItem(int fromPos, int toPos, int amount, InventoryHolderInterface toHolder = null);
        void UseItem(int pos, CharStats charToUse);
        void Organize();
    }
}
