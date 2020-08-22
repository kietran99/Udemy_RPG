using UnityEngine;

namespace RPG.Inventory
{
    public abstract class InventoryAction : MonoBehaviour, InventoryActionInterface
    {
        [SerializeField] protected ActionController actionController = null;

        protected InventoryControllerInterface inventoryController;

        protected virtual void Start()
        {
            if (actionController == null) return;

            inventoryController = actionController.InventoryController;
        }

        public abstract void Invoke();
    }
}
