using UnityEngine;

namespace RPG.Inventory
{
    public class ActionController : MonoBehaviour, IActionController
    {
        public InventoryControllerInterface InventoryController { get { return invController.GetComponent<InventoryControllerInterface>(); } }

        public IAmountSelector AmountSelector { get { return amountSelector.GetComponent<IAmountSelector>(); } }

        public IUserChooser UserChooser { get { return userChooser.GetComponent<IUserChooser>(); } }

        [SerializeField]
        private GameObject interactButtons = null;

        [SerializeField]
        private GameObject invController = null;

        [SerializeField]
        private GameObject amountSelector = null;

        [SerializeField]
        private GameObject userChooser = null;

        void Start()
        {
            AmountSelector.OnActivate += HideInteractButtons;
            AmountSelector.OnDeactivate += () => ShowInteractButtons();

            UserChooser.OnActivate += HideInteractButtons;
            UserChooser.OnDeactivate += ShowInteractButtons;
        }

        public void ShowInteractButtons()
        {
            interactButtons.SetActive(true);
        }

        public void HideInteractButtons()
        {
            interactButtons.SetActive(false);
        }
    }
}
