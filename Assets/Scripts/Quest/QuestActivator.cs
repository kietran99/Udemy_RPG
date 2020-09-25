using UnityEngine;

namespace RPG.Quest
{
    public class QuestActivator : MonoBehaviour
    {
        [SerializeField]
        private QuestData data = null;

        [SerializeField]
        private GameObject questActivateUIObject = null;

        [SerializeField]
        private QuestStatusColor statusColor = null;

        private QuestActivateUI questActivateUI;

        private IQuestTracker tracker;

        private bool canActivate, hasAccepted;

        private void Update()
        {
            if (!canActivate || !Input.GetKeyDown(KeyCode.Q)) return;

            Activate();
        }

        private void Activate()
        {
            if (!hasAccepted)
            {
                Debug.Log("Status: Unaccepted");
                InitUI();
                questActivateUI.gameObject.SetActive(true);
                return;
            }

            if (tracker == null) return;

            if (!tracker.IsComplete()) return;

            GiveRewards();
            statusColor.gameObject.SetActive(false);
        }

        private void InitUI()
        {
            if (questActivateUI != null) return;
            
            questActivateUI = Instantiate(questActivateUIObject).GetComponent<QuestActivateUI>();
            questActivateUI.OnAccept += OnQuestAccept;
            questActivateUI.BindData(data.QuestName, data.QuestDescription, data.ItemReward.Sprite, data.GoldReward);                      
        }

        private void GiveRewards()
        {
            Debug.Log("Give rewards");
            ItemManager.Instance.CurrentGold += data.GoldReward;
            AttemptToObtain();
            tracker = null;
        }

        private void AttemptToObtain()
        {
            Inventory.AbstractItemHolderFactory itemHolderFactory = new Inventory.ItemHolderFactory();
            int invAvail = ItemManager.Instance.AddItem(Inventory.InventoryOwner.BAG, itemHolderFactory.CreateItemToObtainHolder(data.ItemReward));
            if (invAvail == Constants.INVALID) Debug.LogError("INVENTORY FULL!");
        }

        private void OnQuestAccept()
        {
            hasAccepted = true;
            statusColor.UpdateSpriteColor(QuestStatus.ONGOING);

            questActivateUI.OnAccept -= OnQuestAccept;

            if (!ServiceLocator.Resolve<IQuestManager>(out IQuestManager questManager)) return;
            
            tracker = data.QuestGoal.GenerateTracker(data.QuestName);
            questManager.AddTracker(tracker);           
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            canActivate = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            canActivate = false;
        }
    }
}
