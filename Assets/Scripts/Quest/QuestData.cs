using UnityEngine;

namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "RPG Generator/Quests/Quest Data")]
    public class QuestData : ScriptableObject
    {
        #region PROPERTIES
        public string QuestName { get => questName; }
        public string QuestDescription { get => questDescription; }
        public Sprite ItemReward { get => itemReward; }
        public int GoldReward { get => goldReward; }
        #endregion

        [SerializeField]
        private string questName = "", questDescription = "";

        [SerializeField]
        private Sprite itemReward = null;

        [SerializeField]
        private int goldReward = 0;
    }
}
