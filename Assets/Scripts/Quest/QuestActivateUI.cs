using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Quest
{
    public class QuestActivateUI : MonoBehaviour
    {
        [SerializeField]
        private Text questNameText = null, questDescriptionText = null;

        [SerializeField]
        private Image itemRewardImage = null;

        [SerializeField]
        private Text goldRewardAmountText = null;

        public Action OnAccept;

        public void BindData(string questName, string questDescription, Sprite itemReward, int goldReward)
        {
            questNameText.text = questName;
            questDescriptionText.text = questDescription;
            itemRewardImage.sprite = itemReward;
            goldRewardAmountText.text = goldReward.ToString();

            gameObject.SetActive(true);
        }

        public void Accept()
        {
            OnAccept?.Invoke();
            gameObject.SetActive(false);
        }

        public void Decline()
        {
            gameObject.SetActive(false);
        }
    }
}
