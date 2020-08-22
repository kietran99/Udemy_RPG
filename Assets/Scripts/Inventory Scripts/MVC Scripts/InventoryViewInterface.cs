using System;

namespace RPG.Inventory
{
    public interface InventoryViewInterface
    {
        Func<int, DetailData> OnItemButtonClick { get; set; }

        void Display(ItemHolder[] holders);
    }
}
