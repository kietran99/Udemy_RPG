namespace RPG.Quest
{
    public abstract class AbstractQuestTracker : IQuestTracker
    {
        public string QuestName { get; }

        protected AbstractQuestTracker(string questName)
        {
            QuestName = questName;
            TrackEvents();
        }

        protected abstract void TrackEvents();
    }
}