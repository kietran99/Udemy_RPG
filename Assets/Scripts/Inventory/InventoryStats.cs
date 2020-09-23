using System.Collections.Generic;

namespace RPG.Inventory
{
    public class InventoryStats : EventSystems.IEventData
    {
        private Dictionary<System.Type, int> stats;

        public InventoryStats(InventoryHolderInterface holder)
        {
            stats = new Dictionary<System.Type, int>();
            holder.ItemHolders.Map(AddOrUpdateStats);
        }

        private void AddOrUpdateStats(ItemHolder holder)
        {
            var itemType = holder.TheItem.GetType();

            try
            {
                stats.Add(itemType, holder.Amount);
            }
            catch
            {
                stats[itemType] = stats[itemType] + holder.Amount;
            }
        } 
        
        public int LookUp(System.Type itemType)
        {
            if (stats.TryGetValue(itemType, out int amount))
            {
                return amount;
            }

            return Constants.INVALID;
        }
    }
}