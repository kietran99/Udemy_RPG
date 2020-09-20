namespace RPG.Inventory
{
    public class UnequipAction : InventoryAction
    {
        public override void Invoke()
        {
            inventoryController.UnequipItem();
            actionController.EnableButtonsAfterUnequip();
        }
    }
}
