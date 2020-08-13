using UnityEngine.UI;

namespace RPG.Inventory
{
    public interface ItemDetailsInterface
    {
        Text ItemNameText { get; }
        Text ItemDescriptionText { get; }
        void Show(string name, string description);
    }
}
