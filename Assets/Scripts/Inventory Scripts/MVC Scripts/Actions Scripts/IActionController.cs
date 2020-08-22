namespace RPG.Inventory
{
    public interface IActionController
    {
        IAmountSelector AmountSelector { get; }
        IUserChooser UserChooser { get; }
        InventoryControllerInterface InventoryController { get; }

        void ShowInteractButtons();
        void HideInteractButtons();
    }
}