namespace RPG.Inventory
{
    public class OrganizeAction : InventoryAction
    {        
        public override void Invoke()
        {
            ItemManager.Instance.GetInvHolder(invController.CharCycler.CurrPos).Organize();
            invController.ShowInventory();
        }
    }
}
