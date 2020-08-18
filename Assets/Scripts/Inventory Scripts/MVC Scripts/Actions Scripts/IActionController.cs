using UnityEngine;

namespace RPG.Inventory
{
    public interface IActionController
    {
        IAmountSelector AmountSelector { get; }
        InventoryControllerInterface InvController { get; }
    }
}