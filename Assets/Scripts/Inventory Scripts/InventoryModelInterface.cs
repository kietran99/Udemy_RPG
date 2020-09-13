namespace RPG.Inventory
{
    public interface InventoryModelInterface
    {
        ItemHolder[] GetInventory(ItemOwner possessor);
    }
}
