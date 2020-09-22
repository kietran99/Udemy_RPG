namespace RPG.Quest
{
    public abstract class AbstractQuestTracker : IQuestTracker
    {
        public string QuestName { get; set; }

        protected AbstractQuestTracker(string questName)
        {
            QuestName = questName;
        }

        protected abstract void TrackEvents();
    }
}