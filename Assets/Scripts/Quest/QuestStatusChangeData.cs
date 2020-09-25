using EventSystems;

namespace RPG.Quest
{
    public struct QuestStatusChangeData : IEventData
    {
        public QuestStatus status;

        public QuestStatusChangeData(QuestStatus status)
        {
            this.status = status;
        }
    }
}