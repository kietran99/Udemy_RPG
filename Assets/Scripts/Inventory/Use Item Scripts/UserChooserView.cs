using UnityEngine;
using UnityEngine.UI;
using System;

namespace RPG.Inventory
{
    public class UserChooserView : MonoBehaviour, IUserChooserView
    {
        [SerializeField]
        private Text remainingText = null;

        public Func<int, (int, int)> OnItemUse { get; set; }

        private UserButton[] userButtons;

        private IObjectPool userButtonPool;

        void Awake()
        {
            userButtonPool = GetComponent<IObjectPool>();
        }

        public void Init()
        {
            int numOfActives = GameManager.Instance.GetNumActives();
            userButtons = userButtons ?? new UserButton[numOfActives];

            for (int i = 0; i < numOfActives; i++)
            {
                var instance = userButtonPool.Pop();
                userButtons[i] = instance.GetComponent<UserButton>();
                int pos = i;
                userButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                userButtons[i].GetComponent<Button>().onClick.AddListener(() => UpdateStat(pos));
            }
        }

        public void Destruct()
        {
            userButtonPool.Reset();
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
