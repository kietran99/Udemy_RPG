public interface IInventoryHolder
{
    int FindFirstEmptySlot();
    void RemoveAt(int posToRemove, int amount);
    int Add(ItemHolder itemHolderToAdd);
    void AddAt(ItemHolder itemHolderToAdd, int posToAdd);
    ItemHolder[] GetEquippedItems();
    int FindSameEquippedType(Equipment itemToCompare);
    void MoveItem(int fromPos, int toPos, int amount, IInventoryHolder toHolder = null);
    void UseItem(int pos, CharStats charToUse);
    void Organize();
}
