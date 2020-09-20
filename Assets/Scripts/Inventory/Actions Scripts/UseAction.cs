namespace RPG.Inventory
{
    public class UseAction : InventoryAction
    {        
        public override void Invoke()
        {
            actionController.UserChooser.Activate(inventoryController);
        }      
    }
}
