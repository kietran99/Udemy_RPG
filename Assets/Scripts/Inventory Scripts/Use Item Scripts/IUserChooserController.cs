using System;

namespace RPG.Inventory
{
    public interface IUserChooserController
    {
        void Deactivate();

        #region DELEGATES
        Action OnActivate { get; set; }
        Action OnDeactivate { get; set; }
        #endregion
    }
}
