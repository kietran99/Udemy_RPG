using EventSystems;
using UnityEngine;

namespace RPG.Quest
{
    public class ItemTracker : AbstractQuestTracker
    {
        public int AccumulatedAmount { get; set; }

        private readonly ItemGoal goal;

        public ItemTracker(string questName, ItemGoal goal) : base(questName)
        {
            this.goal = goal;            
            Debug.Log("Tracking: " + questName);
        }

        protected override void TrackEvents()
        {
            EventManager.Instance.StartListening<RPG.Inventory.InventoryStats>(UpdateProgress);
        }

        private void UpdateProgress(RPG.Inventory.InventoryStats invStats)
        {
            AccumulatedAmount = invStats.LookUp(goal.ItemType);
            ProcessCompletion();
        }

        private void ProcessCompletion()
        {
            if (AccumulatedAmount < goal.Quantity) return;

            Debug.Log("Completed: " + QuestName);
            EventSystems.EventManager.Instance.TriggerEvent(new QuestCompleteData(QuestName));
            EventManager.Instance.StopListening<RPG.Inventory.InventoryStats>(UpdateProgress);
        }
    }
}