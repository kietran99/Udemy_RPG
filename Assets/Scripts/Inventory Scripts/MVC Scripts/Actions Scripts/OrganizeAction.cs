namespace RPG.Inventory
{
    public class OrganizeAction : InventoryAction
    {        
        public override void OnInvoke()
        {
            ItemManager.Instance.GetInvHolder(invController.CharCycler.CurrPos).Organize();
            invController.ShowInventory();
        }
    }
}
