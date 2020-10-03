using UnityEngine;

namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Item Goal", menuName = "RPG Generator/Quest/Item Goal")]
    public class ItemGoal : AbstractQuestGoal, IQuantityGoal
    {
        #region PROPERTIES
        public int Quantity { get => quantity; }

        public Item Item { get => item; }
        #endregion

        #region FIELDS
        [SerializeField]
        private int quantity = 1;

        [SerializeField]
        private Item item = null;
        #endregion

        public override IQuestTracker GenerateTracker(string questName)
        {
            return new ItemTracker(questName, this);
        }
    }
}
