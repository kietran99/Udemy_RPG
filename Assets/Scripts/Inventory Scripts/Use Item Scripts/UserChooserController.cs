using System;
using UnityEngine;
using Functional;

namespace RPG.Inventory
{
    [RequireComponent(typeof(InteractDisabler))]
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

        private InventoryControllerInterface inventoryController;

        private InteractDisablerInterface interactDisabler;

        private IUserChooserView view;

        private int nRemaining;

        private EntityStats.Attributes changingAttrib;

        private bool isHPMP;

        #region DELEGATES
        public Action OnActivate { get; set; }
        public Action OnDeactivate { get; set; }
        #endregion

        void Awake()
        {
            interactDisabler = GetComponent<InteractDisablerInterface>();
            view = viewObject.GetComponent<IUserChooserView>();
        }

        public void Activate(InventoryControllerInterface inventoryController)
        {
            gameObject.SetActive(true);
            view.Init();

            this.inventoryController = inventoryController;
            interactDisabler.Activate();
            DisplayParty();

            view.OnItemUse += UseItem;
            OnActivate?.Invoke();
        }

        void Update()
        {
            if (!gameObject.activeInHierarchy) return;

            if (Input.GetKeyDown(KeyboardControl.GlobalExit))
            {
                Deactivate();
                gameObject.SetActive(false);
            }
        }

        void OnEnable()
        {
            //view.Init();
            //interactDisabler.Activate();
            //DisplayParty();
            //view.OnItemUse += UseItem;
            //OnActivate?.Invoke();
        }

        void OnDisable()
        {
            //Deactivate();
        }

        public void Deactivate()
        {
            view.Destruct();
            interactDisabler.Deactivate();
            view.OnItemUse -= UseItem;
            OnDeactivate?.Invoke();
        }

        private void DisplayParty()
        {
            CharStats[] party = GameManager.Instance.GetActiveChars();

            Item selectedItem = ItemManager.Instance.GetItemAt(inventoryController.ChosenPosition, inventoryController.CharCycler.Current);
            NumRemaining = ItemManager.Instance.GetNumOfItemsAt(inventoryController.ChosenPosition, inventoryController.CharCycler.Current);

            changingAttrib = selectedItem.Effects[0].Attribute;

            isHPMP = changingAttrib == EntityStats.Attributes.HP ||
                changingAttrib == EntityStats.Attributes.MAX_HP ||
                changingAttrib == EntityStats.Attributes.MP ||
                changingAttrib == EntityStats.Attributes.MAX_MP;
            
            HigherOrderFunc.Map((CharStats stats, int idx) =>
            {
                (int current, int max) = GetStat(changingAttrib, stats);
                view.ShowUserStat(idx, changingAttrib, stats.CharacterName, current, max);
            }, party);
        }        

        private (int cur, int max) GetStat(EntityStats.Attributes attr, CharStats stats)
        {
            switch (attr)
            {
                case EntityStats.Attributes.HP:
                case EntityStats.Attributes.MAX_HP: return (stats.CurrentHP, stats.MaxHP);
                case EntityStats.Attributes.MP:
                case EntityStats.Attributes.MAX_MP: return (stats.CurrentMP, stats.MaxMP);
                case EntityStats.Attributes.STR: return (stats.Strength, Constants.NONE_VALUE);
                case EntityStats.Attributes.DEF: return (stats.Defence, Constants.NONE_VALUE);
                case EntityStats.Attributes.INT: return (stats.Intellect, Constants.NONE_VALUE);
                case EntityStats.Attributes.VIT: return (stats.Vitality, Constants.NONE_VALUE);
                case EntityStats.Attributes.AGI: return (stats.Agility, Constants.NONE_VALUE);
                case EntityStats.Attributes.LCK: return (stats.Luck, Constants.NONE_VALUE);
                case EntityStats.Attributes.EXP: return (stats.CurrentEXP, Constants.NONE_VALUE);
                default: return (0, Constants.NONE_VALUE);
            }
        }
    
        private (int cur, int max) UseItem(int idx)
        {
            CharStats selectedUser = GameManager.Instance.GetCharacterAt(idx);
            ItemManager.Instance.UseItem(inventoryController.CharCycler.Current, inventoryController.ChosenPosition, selectedUser);
            inventoryController.ShowInventory();

            NumRemaining--;

            return GetStat(changingAttrib, selectedUser);
        }
    }
}
