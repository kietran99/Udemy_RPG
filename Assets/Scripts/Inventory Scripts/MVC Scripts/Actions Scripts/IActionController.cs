namespace RPG.Inventory
{
    public interface IActionController
    {
        IAmountSelector AmountSelector { get; }
        IUserChooserController UserChooser { get; }
        InventoryControllerInterface InventoryController { get; }

        void ShowInteractButtons();
        void HideInteractButtons();
    }
}