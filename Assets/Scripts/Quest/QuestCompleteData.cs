namespace RPG.Quest
{
    public struct QuestCompleteData : EventSystems.IEventData
    {
        public string QuestName { get; set; }

        public QuestCompleteData(string questName)
        {
            QuestName = questName;
        }       
    }
}