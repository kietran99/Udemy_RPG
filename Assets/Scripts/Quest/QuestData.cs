using UnityEngine;

namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "RPG Generator/Quest/Quest Data")]
    public class QuestData : ScriptableObject
    {
        #region PROPERTIES
        public string QuestName { get => questName; }
        public string QuestDescription { get => questDescription; }
        public Item ItemReward { get => itemReward; }
        public int GoldReward { get => goldReward; }
        public AbstractQuestGoal QuestGoal { get => questGoal; }
        #endregion

        [SerializeField]
        private string questName = "";
        
        [SerializeField]
        [TextArea]
        private string questDescription = "";

        [SerializeField]
        private Item itemReward = null;

        [SerializeField]
        private int goldReward = 0;

        [SerializeField]
        private AbstractQuestGoal questGoal = null;
    }
}
