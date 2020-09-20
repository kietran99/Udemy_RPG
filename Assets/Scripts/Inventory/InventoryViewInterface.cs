using System;

namespace RPG.Inventory
{
    public interface InventoryViewInterface
    {
        Func<int, DetailsData> OnItemButtonClick { get; set; }

        void Display(ItemHolder[] holders);
        void ShowItemDetails(DetailsData data);
        void Reset();
    }
}
