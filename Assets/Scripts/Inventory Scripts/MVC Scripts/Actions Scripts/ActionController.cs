using UnityEngine;

namespace RPG.Inventory
{
    public class ActionController : MonoBehaviour, IActionController
    {
        #region PROPERTIES
        public InventoryControllerInterface InventoryController { get { return invController.GetComponent<InventoryControllerInterface>(); } }

        public IAmountSelector AmountSelector { get { return amountSelector.GetComponent<IAmountSelector>(); } }

        public IUserChooserController UserChooser { get { return userChooser.GetComponent<IUserChooserController>(); } }
        #endregion

        #region SERIALIZE FIELD
        [SerializeField]
        private GameObject interactButtons = null;

        [SerializeField]
        private GameObject invController = null;

        [SerializeField]
        private GameObject amountSelector = null;

        [SerializeField]
        private GameObject userChooser = null;

        [SerializeField]
        private GameObject useButton = null, equipButton = null;
        #endregion

        void Start()
        {
            AmountSelector.OnActivate += HideInteractButtons;
            AmountSelector.OnDeactivate += ShowInteractButtons;

            UserChooser.OnActivate += HideInteractButtons;
            UserChooser.OnDeactivate += ShowInteractButtons;

            InventoryController.OnUsableItemClick += ToggleUseEquip;
        }

        public void ShowInteractButtons()
        {
            interactButtons.SetActive(true);
        }

        public void HideInteractButtons()
        {
            interactButtons.SetActive(false);
        }

        public void ToggleUseEquip(bool usable)
        {
            useButton.SetActive(usable);
            equipButton.SetActive(!usable);
        }
    }
}
