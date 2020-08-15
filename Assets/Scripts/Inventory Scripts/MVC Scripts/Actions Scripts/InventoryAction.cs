using UnityEngine;

namespace RPG.Inventory
{
    public abstract class InventoryAction : MonoBehaviour, InventoryActionInterface
    {
        [SerializeField]
        protected GameObject invControllerObject = null;

        protected InventoryControllerInterface invController;

        // Start is called before the first frame update
        void Start()
        {
            invController = invControllerObject.GetComponent<InventoryControllerInterface>();
        }

        public abstract void OnInvoke();
    }
}
