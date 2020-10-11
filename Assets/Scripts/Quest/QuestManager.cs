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
            if (trackers.Contains(tracker)) return;

            trackers.Add(tracker);
        }

        public void RemoveTracker(IQuestTracker tracker)
        {
            if (trackers.Contains(tracker)) return;

            tracker.OnUntrack?.Invoke();
            trackers.Remove(tracker);           
        }

        public bool TryFindTracker(string questName, out IQuestTracker tracker)
        {
            var (trackerToFind, idx) = trackers.Lookup(_ => _.QuestName.Equals(questName));
            tracker = trackerToFind;
            return !idx.Equals(Constants.INVALID);
        }        
    }
}