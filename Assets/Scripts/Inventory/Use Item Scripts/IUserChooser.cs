using System;

namespace RPG.Inventory
{
    public interface IUserChooser
    {
        void Activate();

        Action OnActivate { get; set; }
        Action OnDeactivate { get; set; }
    }
}