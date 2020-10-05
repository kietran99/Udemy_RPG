using EventSystems;
using System.Collections.Generic;

namespace RPG.Quest
{
    public class TrackedQuestsData : IEventData
    {        
        public List<IQuestTracker> Trackers { get; private set; }

        public TrackedQuestsData(List<IQuestTracker> trackers)
        {
            Trackers = trackers;
        }
    }
}