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
        private GameObject useButton = null, equipButton = null, unequipButton = null, moveButton = null, discardButton = null;
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

        public void ToggleUseEquip(bool usable, bool isEquipped)
        {
            bool hasChosenEmptySlot = InventoryController.HasChosenEmptySlot();
            
            ToggleInteractability(useButton, !hasChosenEmptySlot);
            ToggleInteractability(equipButton, !hasChosenEmptySlot);
            ToggleInteractability(unequipButton, !hasChosenEmptySlot);
            ToggleInteractability(moveButton, !hasChosenEmptySlot && !isEquipped);
            ToggleInteractability(discardButton, !hasChosenEmptySlot && !isEquipped);

            useButton.SetActive(usable);
            equipButton.SetActive(!usable && !isEquipped);
            unequipButton.SetActive(!usable && isEquipped);
        }

        private void ToggleInteractability(GameObject button, bool interactable)
        {
            button.GetComponent<Button>().interactable = interactable;
        }
        
        public void EnableButtonsAfterUnequip()
        {
            equipButton.SetActive(true);
            unequipButton.SetActive(false);
            ToggleInteractability(moveButton, true);
            ToggleInteractability(discardButton, true);
        }
    }
}
