using System;
using UnityEngine;

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

        public void Deactivate()
        {
            view.Destruct();
            interactDisabler.Deactivate();
            OnDeactivate?.Invoke();
        }

        private void DisplayParty()
        {
            CharStats[] party = GameManager.Instance.GetActiveChars();

            Item selectedItem = ItemManager.Instance.GetItemAt(inventoryController.ChosenPosition, inventoryController.CharCycler.Current);
            NumRemaining = ItemManager.Instance.GetNumOfItemsAt(inventoryController.ChosenPosition, inventoryController.CharCycler.Current);

            changingAttrib = selectedItem.Effects[0].Attribute;

            if (changingAttrib == EntityStats.Attributes.HP ||
                changingAttrib == EntityStats.Attributes.MAX_HP ||
                changingAttrib == EntityStats.Attributes.MP ||
                changingAttrib == EntityStats.Attributes.MAX_MP)
            {
                isHPMP = true;

                for (int i = 0; i < party.Length; i++)
                {
                    (int current, int max) temp = GetHPMP(changingAttrib, party[i]);
                    view.ShowUserStat(i, party[i].CharacterName, temp.current, temp.max, changingAttrib);
                }
            }
            else
            {
                isHPMP = false;

                for (int i = 0; i < party.Length; i++)
                {
                    view.ShowUserStat(i, party[i].CharacterName, GetStat(changingAttrib, party[i]), changingAttrib);
                }
            }
        }

        private int GetStat(EntityStats.Attributes attr, CharStats stats)
        {
            switch (attr)
            {
                case EntityStats.Attributes.STR: return stats.Strength;
                case EntityStats.Attributes.DEF: return stats.Defence;
                case EntityStats.Attributes.INT: return stats.Intellect;
                case EntityStats.Attributes.VIT: return stats.Vitality;
                case EntityStats.Attributes.AGI: return stats.Agility;
                case EntityStats.Attributes.LCK: return stats.Luck;
                case EntityStats.Attributes.EXP: return stats.CurrentEXP;
                default: return -1;
            }
        }

        private (int cur, int max) GetHPMP(EntityStats.Attributes attr, CharStats stats)
        {
            switch (attr)
            {
                case EntityStats.Attributes.HP:
                case EntityStats.Attributes.MAX_HP: return (stats.CurrentHP, stats.MaxHP);
                case EntityStats.Attributes.MP:
                case EntityStats.Attributes.MAX_MP: return (stats.CurrentMP, stats.MaxMP);
                default: return (0, 0);
            }
        }
    }
}
