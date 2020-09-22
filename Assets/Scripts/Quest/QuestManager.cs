using System.Collections.Generic;

namespace RPG.Quest
{
    public class QuestManager : IQuestManager
    {
        private List<IQuestTracker> trackers;

        public QuestManager()
        {
            trackers = new List<IQuestTracker>();
        }

        public void AddTracker(IQuestTracker tracker)
        {
            trackers.Add(tracker);
        }

        public void RemoveTracker(IQuestTracker tracker)
        {
            trackers.Remove(tracker);
        }
    }
}