using System;

namespace RPG.Inventory
{
    public interface IUserChooserController
    {
        void Activate(InventoryControllerInterface inventoryController);
        void Deactivate();

        #region DELEGATES
        Action OnActivate { get; set; }
        Action OnDeactivate { get; set; }
        #endregion
    }
}
