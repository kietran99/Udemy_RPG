namespace RPG.Inventory
{
    public interface InventoryModelInterface
    {
        ItemHolder[] GetInventory(InventoryOwner possessor);
    }
}
