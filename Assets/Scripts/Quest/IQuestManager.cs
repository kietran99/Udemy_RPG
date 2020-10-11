using System.Collections.Generic;

namespace RPG.Quest
{
    public interface IQuestManager
    {
        void AddTracker(IQuestTracker tracker);
        void RemoveTracker(IQuestTracker tracker);
        bool TryFindTracker(string questName, out IQuestTracker tracker);
    }
}