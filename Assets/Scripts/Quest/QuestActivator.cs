using EventSystems;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Quest
{
    public class QuestActivator : MonoBehaviour
    {
        #region SERIALIZE FIELDS
        [SerializeField]
        private QuestData data = null;

        [SerializeField]
        private GameObject questActivateUIObject = null;

        [SerializeField]
        private QuestStatusBubbleChat statusBubbleChat = null;
        #endregion

        #region FIELDS
        private IQuestManager questManager;

        private QuestActivateUI questActivateUI;

        private IQuestTracker tracker;

        private bool canActivate, hasAccepted;
        #endregion
        
        private void Start()
        {
            Debug.Log("Start listening");
            EventManager.Instance.StartListening<TrackedQuestsData>(LoadQuestData);
        }

        private void OnDestroy()
        {
            EventManager.Instance.StopListening<TrackedQuestsData>(LoadQuestData);
        }

        private void Update()
        {
            if (questManager == null)
            {
                ServiceLocator.Resolve<IQuestManager>(out questManager);
            }

            if (!canActivate || !Input.GetKeyDown(KeyCode.Q)) return;

            if (!hasAccepted)
            {
                Activate();
                return;
            }

            if (tracker == null) return;

            if (!tracker.IsComplete()) return;

            ProcessQuestComplete();            
        }

        private void Activate()
        {
            Debug.Log("Status: Unaccepted");
            InitUI();
            questActivateUI.gameObject.SetActive(true);
        }

        private void InitUI()
        {
            if (questActivateUI != null) return;
            
            questActivateUI = Instantiate(questActivateUIObject).GetComponent<QuestActivateUI>();
            questActivateUI.OnAccept += ProcessQuestAccept;
            questActivateUI.BindData(data.QuestName, data.QuestDescription, data.ItemReward.Sprite, data.GoldReward);                      
        }
        
        private bool AttemptToObtain()
        {
            Inventory.AbstractItemHolderFactory itemHolderFactory = new Inventory.ItemHolderFactory();
            int invAvail = ItemManager.Instance.AddItem(Inventory.InventoryOwner.BAG, itemHolderFactory.CreateItemToObtainHolder(data.ItemReward));
            if (invAvail == Constants.INVALID) Debug.LogError("INVENTORY FULL!");
            return invAvail != Constants.INVALID;
        }

        private void ProcessQuestAccept()
        {
            Debug.Log("Status: Accepted");
            hasAccepted = true;
            statusBubbleChat.UpdateSpriteColor(QuestStatus.ONGOING);
            questActivateUI.OnAccept -= ProcessQuestAccept;           
            tracker = data.QuestGoal.GenerateTracker(data.QuestName);
            questManager.AddTracker(SceneManager.GetActiveScene().name, tracker);

            //if (!tracker.IsComplete())
            //{
            //    questManager.AddTracker(SceneManager.GetActiveScene().name, tracker);
            //    return;
            //}

            //ProcessQuestComplete();
        }

        private void ProcessQuestComplete()
        {
            Debug.Log("Status: Complete");

            if (!AttemptToObtain()) return;

            GameManager.Instance.IncreaseGold(data.GoldReward);
            questManager.RemoveTracker(SceneManager.GetActiveScene().name, tracker);
            tracker = null;
            statusBubbleChat.gameObject.SetActive(false);
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

        private void LoadQuestData(TrackedQuestsData cache)
        {
            var (cachedTracker, idx) = cache.Trackers.Lookup(_ => _.QuestName.Equals(data.QuestName));

            if (idx.Equals(Constants.INVALID)) return;

            tracker = cachedTracker;

        }

    }
}
