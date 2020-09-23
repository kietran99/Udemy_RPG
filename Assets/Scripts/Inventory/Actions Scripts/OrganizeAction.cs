namespace RPG.Inventory
{
    public class OrganizeAction : InventoryAction
    {        
        public override void Invoke()
        {
            inventoryController.Organize();
        }
    }
}
