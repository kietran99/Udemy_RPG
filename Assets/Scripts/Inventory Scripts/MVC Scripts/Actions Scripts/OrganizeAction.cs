namespace RPG.Inventory
{
    public class OrganizeAction : InventoryAction
    {        
        public override void Invoke()
        {
            ItemManager.Instance.GetInvHolder(inventoryController.CharCycler.Current).Organize();
            inventoryController.ShowInventory();
        }
    }
}
