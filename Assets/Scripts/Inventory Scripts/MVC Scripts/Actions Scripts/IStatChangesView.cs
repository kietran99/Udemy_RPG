using System;

namespace RPG.Inventory
{
    public interface IStatChangesView
    {
        Action OnActivate { get; set; }
        Action OnDeactivate { get; set; }

        void Activate();
        void Show((int cur, int after)[] changes);
        void Deactivate();
        void Confirm();
        void Cancel();
    }
}
