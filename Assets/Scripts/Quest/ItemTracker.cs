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
            AccumulatedAmount = invStats.LookUp(goal.Item.ItemName);

            if (AccumulatedAmount < goal.Quantity)
            {
                Debug.Log("Status: Ongoing");
                EventManager.Instance.TriggerEvent(new QuestStatusChangeData(QuestStatus.ONGOING));
                return;
            }

            Debug.Log("Status: Completed");
            EventManager.Instance.TriggerEvent(new QuestStatusChangeData(QuestStatus.COMPLETED));
        }
        
        public override bool IsComplete() => AccumulatedAmount >= goal.Quantity;

        protected override void UntrackEvents()
        {
            EventManager.Instance.StopListening<RPG.Inventory.InventoryStats>(UpdateProgress);
        }
    }
}