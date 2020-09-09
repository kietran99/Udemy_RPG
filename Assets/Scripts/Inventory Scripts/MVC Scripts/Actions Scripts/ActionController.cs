using UnityEngine;
using UnityEngine.UI;

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
        private StatChangesView statChangesView = null;

        [SerializeField]
        private GameObject useButton = null, equipButton = null;
        #endregion

        void Start()
        {
            AmountSelector.OnActivate += HideInteractors;
            AmountSelector.OnDeactivate += ShowInteractors;

            UserChooser.OnActivate += HideInteractors;
            UserChooser.OnDeactivate += ShowInteractors;

            statChangesView.OnActivate += HideInteractors;
            statChangesView.OnDeactivate += ShowInteractors;

            InventoryController.OnUsableItemClick += ToggleUseEquip;
        }

        public void ShowInteractors()
        {
            interactButtons.SetActive(true);
        }

        public void HideInteractors()
        {
            interactButtons.SetActive(false);
        }

        public void ToggleUseEquip(bool usable)
        {
            bool hasChosenEmptySlot = InventoryController.HasChosenEmptySlot();
            
            useButton.GetComponent<Button>().interactable = !hasChosenEmptySlot;
            equipButton.GetComponent<Button>().interactable = !hasChosenEmptySlot;

            useButton.SetActive(usable);
            equipButton.SetActive(!usable);
        }
    }
}
