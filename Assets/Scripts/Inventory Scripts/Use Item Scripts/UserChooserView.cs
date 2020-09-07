using UnityEngine;
using UnityEngine.UI;
using Functional;
using System;

namespace RPG.Inventory
{
    public class UserChooserView : MonoBehaviour, IUserChooserView
    {
        [SerializeField]
        private GameObject userButtonPrefab = null, buttonsContainer = null;

        [SerializeField]
        private Text remainingText = null;

        private UserButton[] userButtons;

        public Func<int, (int, int)> OnItemUse { get; set; }

        public void Init()
        {
            int numOfActives = GameManager.Instance.GetNumActives();
            userButtons = new UserButton[numOfActives];

            for (int i = 0; i < numOfActives; i++)
            {
                GameObject instance = Instantiate(userButtonPrefab, buttonsContainer.transform);
                userButtons[i] = instance.GetComponent<UserButton>();
                int pos = i;
                userButtons[i].GetComponent<Button>().onClick.AddListener(() => UpdateStat(pos));
            }
        }

        public void Destruct()
        {            
            HOF.Map((UserButton button) => Destroy(button.gameObject), userButtons);
        }

        public void UpdateRemaining(int nRemaining)
        {
            remainingText.text = nRemaining + " left";
        }

        public void ShowUserStat(int idx, EntityStats.Attributes attr, string userName, int curStat, int maxStat = -1)
        {
            userButtons[idx].InitStat(attr, userName, curStat, maxStat);
            userButtons[idx].gameObject.SetActive(true);
        }     
        
        private void UpdateStat(int idx)
        {
            var stats = OnItemUse?.Invoke(idx);
            userButtons[idx].UpdateStat(stats.Value.Item1, stats.Value.Item2);
        }
    }
}
