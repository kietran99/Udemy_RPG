namespace RPG.Inventory
{
    public class DiscardAction : InventoryAction
    {
        private IAmountSelector amountSelector;
        
        public override void Invoke()
        {
            if (inventoryController.HasChosenEmptySlot()) return;

            amountSelector = actionController.AmountSelector;
            amountSelector.OnActivate += BindDelegates;
            amountSelector.OnDeactivate += UnbindDelegates;
            amountSelector.Activate(inventoryController.ChosenItemHolder.Amount);
        }

        void BindDelegates()
        {
            amountSelector.OnAmountConfirm += DiscardItem;
        }

        void UnbindDelegates()
        {
            amountSelector.OnAmountConfirm -= DiscardItem;
            amountSelector.OnActivate -= BindDelegates;
            amountSelector.OnDeactivate -= UnbindDelegates;
        }

        void DiscardItem(int amount)
        {
            actionController.InventoryController.Discard(amount);
        }
    }
}
