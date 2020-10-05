using System.Collections.Generic;

namespace RPG.Quest
{
    public interface IQuestManager
    {
        List<IQuestTracker> GetTrackers(string sceneName);
        void AddTracker(IQuestTracker tracker);
        void RemoveTracker(IQuestTracker tracker);
        void AddTracker(string sceneName, IQuestTracker tracker);
        void RemoveTracker(string sceneName, IQuestTracker tracker);
    }
}