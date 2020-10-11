using System;

namespace RPG.Quest
{
    public abstract class AbstractQuestTracker : IQuestTracker
    {
        public string QuestName { get; private set; }

        public Action OnUntrack { get; set; }

        protected AbstractQuestTracker(string questName)
        {
            QuestName = questName;
            TrackEvents();
            OnUntrack += ProcessUntrack;
        }

        protected void ProcessUntrack()
        {
            UntrackEvents();
            OnUntrack -= ProcessUntrack;
        }

        protected abstract void TrackEvents();
        protected abstract void UntrackEvents();
        public abstract bool IsComplete();

        
    }
}