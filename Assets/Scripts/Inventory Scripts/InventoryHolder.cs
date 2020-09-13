using System;
using System.Collections.Generic;

namespace RPG.Inventory
{
    public class InventoryHolder : InventoryHolderInterface
    {
        #region PROPERTIES
        public ItemOwner Possessor { get { return possessor; } set { possessor = value; } }
        public ItemHolder[] ItemHolders { get { return itemHolders; } set { itemHolders = value; } }
        public ItemHolder this[int i] => itemHolders[i];
        #endregion

        #region FIELDS
        private ItemOwner possessor;

        private ItemHolder[] itemHolders;

        private ItemHolder nullHolder;
        #endregion

        public InventoryHolder(ItemOwner possessor, int size, ItemHolder nullHolder)
        {
            this.possessor = possessor;
            itemHolders = new ItemHolder[size];
            this.nullHolder = nullHolder;
        }

        private int CheckExists(ItemHolder itemToCheck)
        {
            return itemHolders.LookUp(_ => !ReferenceEquals(_, itemToCheck) && itemToCheck.CompareItem(_)).idx;
        }

        public int FindFirstEmptySlot() => itemHolders.LookUp(holder => holder.Amount <= 0).idx;

        public void RemoveAt(int posToRemove, int amount)
        {
            itemHolders[posToRemove].Amount -= amount;
            if (itemHolders[posToRemove].Amount <= 0) itemHolders[posToRemove] = nullHolder;
        }

        public int Add(ItemHolder itemHolderToAdd)
        {
            int posToCheck = CheckExists(itemHolderToAdd);

            if (posToCheck > -1) AddToExisting(itemHolderToAdd, posToCheck);
            else
            {
                int emptySlot = FindFirstEmptySlot();
                if (emptySlot == Constants.INVALID) return Constants.INVALID;
                itemHolders[emptySlot] = itemHolderToAdd;
            }

            return 0;
        }

        public void AddAt(ItemHolder itemHolderToAdd, int posToAdd)
        {
            if (itemHolders[posToAdd].IsEmpty())
            {
                itemHolders[posToAdd] = itemHolderToAdd;
                return;
            }

            AddToExisting(itemHolderToAdd, posToAdd);
        }

        private void AddToExisting(ItemHolder itemHolderToAdd, int posToAdd)
        {
            itemHolders[posToAdd].Amount += itemHolderToAdd.Amount;

            if (itemHolders[posToAdd].Amount <= ItemHolder.ITEM_CAPACITY) return;

            itemHolders[FindFirstEmptySlot()] = new ItemHolder(itemHolderToAdd.TheItem, itemHolders[posToAdd].Amount - ItemHolder.ITEM_CAPACITY);
            itemHolders[posToAdd].Amount = ItemHolder.ITEM_CAPACITY;
        }

        public int FindSameEquippedTypePos(int posToCompare)
        {
            return itemHolders.LookUp((holder, idx) =>
            holder.IsEquipped && holder.CompareType(this[posToCompare]) && !idx.Equals(posToCompare)).idx;
        }

        public void MoveItem(int fromPos, int toPos, int amount, InventoryHolderInterface toHolder = null)
        {
            if (amount <= 0 || itemHolders[fromPos].IsEmpty() || itemHolders[fromPos].IsEquipped) return;

            int amountToMove = amount, destAmount = itemHolders[toPos].Amount;

            if (destAmount + amount > ItemHolder.ITEM_CAPACITY) amountToMove = ItemHolder.ITEM_CAPACITY - destAmount;

            var factory = new ItemHolderFactory();

            ItemHolder holderToMove = itemHolders[fromPos].IsEquipped ? factory.CreateEquipmentHolder(itemHolders[fromPos].TheItem) :
                                                             factory.CreateRegularHolder(itemHolders[fromPos].TheItem, amountToMove);

            if (toHolder == null) AddAt(holderToMove, toPos);
            else toHolder.AddAt(holderToMove, toPos);

            RemoveAt(fromPos, amountToMove);
        }

        public void UseItem(int pos, CharStats charToUse)
        {
            itemHolders[pos].Use(charToUse);
            RemoveAt(pos, 1);
        }

        public void Organize()
        {
            var sortedItems = new Dictionary<string, int>();
            var emptySlots = new Queue<int>();

            for (int i = 0; i < itemHolders.Length; i++)
            {
                if (itemHolders[i].IsEmpty())
                {
                    emptySlots.Enqueue(i);
                    continue;
                }

                string itemName = itemHolders[i].TheItem.ItemName;
                try
                {
                    sortedItems.Add(itemName, i);
                }
                catch (ArgumentException)
                {
                    MoveItem(i, sortedItems[itemName], itemHolders[i].Amount);

                    if (itemHolders[sortedItems[itemName]].IsFull()) sortedItems.Remove("itemName");

                    if (itemHolders[i].IsEmpty()) emptySlots.Enqueue(i);
                    else sortedItems[itemName] = i;
                }
                finally
                {
                    if (emptySlots.Count != 0 && !itemHolders[i].IsEmpty())
                    {
                        int emptySlot = emptySlots.Dequeue();
                        MoveItem(i, emptySlot, itemHolders[i].Amount);
                        sortedItems[itemName] = emptySlot;
                        emptySlots.Enqueue(i);
                    }
                }
            }
        }
    }
}
