using System;
using UnityEngine;
using Functional;
using KeyboardControl;

namespace RPG.Inventory
{
    public class UserChooserController : MonoBehaviour, IUserChooserController
    {
        private int NumRemaining
        {
            get { return nRemaining; }
            set
            {
                nRemaining = value;

                if (nRemaining == Constants.EMPTY)
                {
                    Deactivate();
                    gameObject.SetActive(false);
                    return;
                }

                view.UpdateRemaining(nRemaining);
            }
        }

        [SerializeField] 
        private GameObject viewObject = null;

        #region PRIVATE
        private InventoryControllerInterface inventoryController;

        private IUserChooserView view;

        private int nRemaining;

        private EntityStats.Attributes changingAttrib;

        private StatFetcher statFetcher;
        #endregion

        #region DELEGATES
        public Action OnActivate { get; set; }
        public Action OnDeactivate { get; set; }
        #endregion

        void Awake()
        {
            view = viewObject.GetComponent<IUserChooserView>();
        }

        public void Activate(InventoryControllerInterface inventoryController)
        {
            gameObject.SetActive(true);
            view.Init();

            this.inventoryController = inventoryController;
            DisplayParty();

            view.OnItemUse += UseItem;
            OnActivate?.Invoke();
        }

        void Update()
        {
            if (!gameObject.activeInHierarchy) return;

            if (Input.GetKeyDown(General.Exit))
            {
                Deactivate();
                gameObject.SetActive(false);
            }
        }
       
        public void Deactivate()
        {
            view.Destruct();
            view.OnItemUse -= UseItem;
            OnDeactivate?.Invoke();
        }

        private void DisplayParty()
        {
            CharStats[] party = GameManager.Instance.GetActiveChars();

            Item selectedItem = ItemManager.Instance.GetItemAt(inventoryController.ChosenPosition, inventoryController.CharCycler.Current);
            NumRemaining = ItemManager.Instance.GetNumOfItemsAt(inventoryController.ChosenPosition, inventoryController.CharCycler.Current);

            changingAttrib = selectedItem.Effects[0].Attribute;
            statFetcher = new StatFetcher(changingAttrib);
            
            HOF.Map((CharStats stats, int idx) =>
            {
                (int current, int max) = statFetcher.ExtractValues(stats);
                view.ShowUserStat(idx, changingAttrib, stats.CharacterName, current, max);
            }, party);
        }        
        
        private (int cur, int max) UseItem(int idx)
        {
            CharStats selectedUserStats = GameManager.Instance.GetCharacterAt(idx);
            ItemManager.Instance.UseItem(inventoryController.CharCycler.Current, inventoryController.ChosenPosition, selectedUserStats);
            inventoryController.ShowInventory();

            NumRemaining--;

            return statFetcher.ExtractValues(selectedUserStats);
        }
    }
}
