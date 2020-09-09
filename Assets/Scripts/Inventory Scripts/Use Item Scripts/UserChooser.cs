using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    [RequireComponent(typeof(InteractDisabler))]
    public class UserChooser : MonoBehaviour, IUserChooser
    {
        [SerializeField]
        private GameObject userButtonPrefab = null, buttonsContainer = null;

        [SerializeField]
        private Text remainingText = null;

        private int numOfRemaining;

        private UserButton[] userButtons;

        private EntityStats.Attributes changingAttr;

        private bool isHPMP;

        #region DELEGATES
        public Action OnActivate { get; set; }
        public Action OnDeactivate { get; set; }
        #endregion
       
        public void Activate()
        {
            gameObject.SetActive(true);

            OnActivate?.Invoke();
        }

        void Update()
        {
            if (!gameObject.activeInHierarchy) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
                gameObject.SetActive(false);
            }
        }

        private void Cancel()
        {
            for (int i = 0; i < userButtons.Length; i++)
            {
                if (i != 0) Destroy(userButtons[i].gameObject);
                else userButtonPrefab.SetActive(false);
            }

            OnDeactivate?.Invoke();
        }

        private void OnEnable()
        {
            InitUserButtons();
            DisplayParty();
        }

        private void OnDisable()
        {
            Cancel();
        }
        
        private void InitUserButtons()
        {
            int numOfActives = GameManager.Instance.GetNumActives();
            userButtons = new UserButton[numOfActives];

            for (int i = 0; i < numOfActives; i++)
            {
                GameObject temp = Instantiate(userButtonPrefab);
                temp.transform.SetParent(buttonsContainer.transform);
                userButtons[i] = temp.GetComponent<UserButton>();
                int pos = i;
                userButtons[i].GetComponent<Button>().onClick.AddListener(() => OnUserSelected(pos));
            }
        }

        struct HPMP
        {
            public int current, max;
            public HPMP(int current, int max)
            {
                this.current = current;
                this.max = max;
            }
        }

        private void DisplayParty()
        {
            CharStats[] party = GameManager.Instance.GetActiveChars();

            //Item selectedItem = ItemManager.Instance.GetItemAt(view.SelectedPos, view.CurrentPossessor);
            Item selectedItem = ItemManager.Instance.GetItemAt(0, ItemPossessor.BAG);
            //numOfRemaining = ItemManager.Instance.GetNumOfItemsAt(view.SelectedPos, view.CurrentPossessor);
            UpdateRemaining();

            changingAttr = selectedItem.Effects[0].Attribute;

            if (changingAttr == EntityStats.Attributes.HP ||
                changingAttr == EntityStats.Attributes.MAX_HP ||
                changingAttr == EntityStats.Attributes.MP ||
                changingAttr == EntityStats.Attributes.MAX_MP)
            {
                isHPMP = true;

                for (int i = 0; i < party.Length; i++)
                {
                    HPMP temp = GetHPMP(changingAttr, party[i]);
                    userButtons[i].InitStat(changingAttr, party[i].CharacterName, temp.current, temp.max);
                }
            }
            else
            {
                isHPMP = false;

                for (int i = 0; i < party.Length; i++)
                {
                    userButtons[i].InitStat(changingAttr, party[i].CharacterName, GetStat(changingAttr, party[i]));
                }
            }
        }

        private int GetStat(EntityStats.Attributes attr, CharStats stats)
        {
            switch (attr)
            {
                case EntityStats.Attributes.STR: return stats.Strength;
                case EntityStats.Attributes.DEF: return stats.Defense;
                case EntityStats.Attributes.INT: return stats.Intellect;
                case EntityStats.Attributes.VIT: return stats.Vitality;
                case EntityStats.Attributes.AGI: return stats.Agility;
                case EntityStats.Attributes.LCK: return stats.Luck;
                case EntityStats.Attributes.EXP: return stats.CurrentEXP;
                default: return -1;
            }
        }

        private HPMP GetHPMP(EntityStats.Attributes attr, CharStats stats)
        {
            switch (attr)
            {
                case EntityStats.Attributes.HP:
                case EntityStats.Attributes.MAX_HP: return new HPMP(stats.CurrentHP, stats.MaxHP);
                case EntityStats.Attributes.MP:
                case EntityStats.Attributes.MAX_MP: return new HPMP(stats.CurrentMP, stats.MaxMP);
                default: return new HPMP(0, 0);
            }
        }

        public void OnUserSelected(int pos)
        {
            CharStats selectedUser = GameManager.Instance.GetCharacterAt(pos);
            //ItemManager.Instance.UseItem(view.CurrentPossessor, view.SelectedPos, selectedUser);
            //view.DisplayAll();

            numOfRemaining--;

            if (numOfRemaining == 0)
            {
                Cancel();
                gameObject.SetActive(false);
                return;
            }

            UpdateRemaining();

            if (isHPMP)
            {
                HPMP temp = GetHPMP(changingAttr, selectedUser);
                userButtons[pos].UpdateStat(temp.current, temp.max);
                return;
            }
            
            userButtons[pos].UpdateStat(GetStat(changingAttr, selectedUser), -1);
        }

        private void UpdateRemaining()
        {
            remainingText.text = numOfRemaining + " left";
        }        
    }
}
