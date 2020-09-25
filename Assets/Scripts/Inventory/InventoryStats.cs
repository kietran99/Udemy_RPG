using System.Collections.Generic;

namespace RPG.Inventory
{
    public class InventoryStats : EventSystems.IEventData
    {
        private readonly Dictionary<string, int> stats;

        public InventoryStats(InventoryHolderInterface holder)
        {
            stats = new Dictionary<string, int>();
            holder.ItemHolders.Map(AddOrUpdateStats);
        }

        private void AddOrUpdateStats(ItemHolder holder)
        {
            if (holder.TheItem.ItemName.Equals(string.Empty)) return;

            var itemName = holder.TheItem.ItemName;

            try
            {
                stats.Add(itemName, holder.Amount);
            }
            catch
            {
                stats[itemName] = stats[itemName] + holder.Amount;
            }
        } 
        
        public int LookUp(string itemName)
        {
            if (stats.TryGetValue(itemName, out int amount))
            {
                return amount;
            }

            return Constants.INVALID;
        }
    }
}