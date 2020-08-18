using UnityEngine;

namespace RPG.Inventory
{
    public abstract class InventoryAction : MonoBehaviour, InventoryActionInterface
    {
        [SerializeField] protected ActionController actionController = null;

        protected InventoryControllerInterface invController;

        protected virtual void Start()
        {
            if (actionController == null) return;

            invController = actionController.InvController;
        }

        public abstract void Invoke();
    }
}
