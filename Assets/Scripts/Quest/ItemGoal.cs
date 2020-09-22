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

        [SerializeField]
        private int quantity = 1;

        [SerializeField]
        private Item item = null;

        public override IQuestTracker GenerateTracker()
        {
            return new ItemTracker("Quest Name", this);
        }
    }
}
