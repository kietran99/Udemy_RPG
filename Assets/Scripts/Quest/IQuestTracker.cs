using System;

namespace RPG.Quest
{
    public interface IQuestTracker
    {
        string QuestName { get; }
        bool IsComplete();

        Action OnUntrack { get; set; }
    }
}