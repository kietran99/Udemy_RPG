using UnityEngine;

namespace RPG.Quest
{
    public abstract class AbstractQuestGoal : ScriptableObject
    {
        public abstract IQuestTracker GenerateTracker();
    }
}