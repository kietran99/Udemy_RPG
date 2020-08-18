namespace RPG.Inventory
{
    public class DiscardAction : InventoryAction
    {
        private IAmountSelector amountSelector;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            amountSelector = actionController.AmountSelector;
        }

        public override void Invoke()
        {
            if (invController.ChosenPosition < 0) return;

            amountSelector.Activate(invController.View, invController.ChosenItemHolder.Amount);
        }
    }
}
