using UnityEngine;

namespace RPG.Inventory
{
    public class ActionController : MonoBehaviour, IActionController
    {
        public InventoryControllerInterface InvController { get { return invController.GetComponent<InventoryControllerInterface>(); } }

        public IAmountSelector AmountSelector { get { return amountSelector.GetComponent<IAmountSelector>(); } }

        [SerializeField]
        private GameObject interactButtons = null;

        [SerializeField]
        private GameObject invController = null;

        [SerializeField]
        private GameObject amountSelector = null;

        void Start()
        {
            AmountSelector.OnActivate += () => interactButtons.SetActive(false);
            AmountSelector.OnAmountConfirm += (int amount) => { InvController.DiscardItem(amount); interactButtons.SetActive(true); };
        }
    }
}
