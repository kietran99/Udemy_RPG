using UnityEngine;

namespace RPG.Quest
{
    public class ItemTracker : AbstractQuestTracker
    {
        private ItemGoal goal;

        public ItemTracker(string questName, ItemGoal goal) : base(questName)
        {
            this.goal = goal;
            Debug.Log(questName + " is being tracked");
        }
    }
}