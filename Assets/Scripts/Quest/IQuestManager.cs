namespace RPG.Quest
{
    public interface IQuestManager
    {
        void AddTracker(IQuestTracker tracker);
        void RemoveTracker(IQuestTracker tracker);
    }
}