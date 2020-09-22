using System;

namespace RPG.Quest
{
    public interface IQuestTracker
    {
        string QuestName { get; set; }
        //Action<string> OnComplete { get; set; }
    }
}