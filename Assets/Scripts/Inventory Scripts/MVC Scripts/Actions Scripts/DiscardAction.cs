namespace RPG.Inventory
{
    public class DiscardAction : InventoryAction
    {
        public override void Invoke()
        {
            if (inventoryController.HasChosenEmptySlot()) return;

            actionController.AmountSelector.Activate(inventoryController.View, inventoryController.ChosenItemHolder.Amount);
        }
    }
}
