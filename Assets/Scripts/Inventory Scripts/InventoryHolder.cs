using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryHolder
{
    #region
    public PossessorSearcher.ItemPossessor Possessor { get { return possessor; } set { possessor = value; } }
    public ItemHolder[] ItemHolders { get { return itemHolders; } set { itemHolders = value; } }
    #endregion

    public const int POSITION_INVALID = -1;

    private PossessorSearcher.ItemPossessor possessor;

    private ItemHolder[] itemHolders;

    private ItemHolder nullHolder;

    public InventoryHolder(PossessorSearcher.ItemPossessor possessor, int size, ItemHolder nullHolder)
    {
        this.possessor = possessor;
        itemHolders = new ItemHolder[size];
        this.nullHolder = nullHolder;
    }      

    private int CheckExists(ItemHolder itemToCheck)
    {
        int posToSkip = System.Array.IndexOf(itemHolders, itemToCheck);

        for (int i = 0; i < itemHolders.Length; i++)
        {
            if (i == posToSkip) continue;
            if (itemToCheck.TheItem.IsEqual(itemHolders[i].TheItem)) return i;
        }

        return POSITION_INVALID;
    }

    public int FindFirstEmptySlot()
    {
        for (int i = 0; i < itemHolders.Length; i++)
        {
            if (itemHolders[i].Amount <= 0) return i;
        }

        return POSITION_INVALID;
    }   

    public void RemoveAt(int posToRemove, int amount)
    {
        itemHolders[posToRemove].Amount -= amount;
        if (itemHolders[posToRemove].Amount <= 0) itemHolders[posToRemove] = nullHolder;
    }

    public int Add(ItemHolder itemHolderToAdd)
    {
        int posToCheck = CheckExists(itemHolderToAdd);
        if (posToCheck > -1) AddToExistingItem(itemHolderToAdd, posToCheck);
        else
        {
            int emptySlot = FindFirstEmptySlot();
            if (emptySlot == POSITION_INVALID) return POSITION_INVALID;
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

        AddToExistingItem(itemHolderToAdd, posToAdd);
    }

    private void AddToExistingItem(ItemHolder itemHolderToAdd, int posToAdd)
    {
        itemHolders[posToAdd].Amount += itemHolderToAdd.Amount;

        if (itemHolders[posToAdd].Amount > ItemHolder.ITEM_CAPACITY)
        {
            itemHolders[FindFirstEmptySlot()] = new ItemHolder(itemHolderToAdd.TheItem, itemHolders[posToAdd].Amount - ItemHolder.ITEM_CAPACITY);
            itemHolders[posToAdd].Amount = ItemHolder.ITEM_CAPACITY;
        }
    }

    public ItemHolder[] GetEquippedItems()
    {
        return itemHolders.Where(x => x.IsEquipped).ToArray();
    }

    public int FindSameEquippedType(Equipment itemToCompare)
    {
        ItemHolder[] equippedItems = GetEquippedItems();

        for (int i = 0; i < equippedItems.Length; i++)
        {
            if (equippedItems[i].TheItem.GetType() == itemToCompare.GetType()) return i;
        }

        return POSITION_INVALID;
    }

    public void MoveItem(int fromPos, int toPos, int amount, InventoryHolder toHolder = null)
    {
        if (amount <= 0) return;

        ItemHolder holderToMove = new ItemHolderFactory().CreateRegularHolder(itemHolders[fromPos].TheItem, amount);

        if (toHolder == null) AddAt(holderToMove, toPos);
        else toHolder.AddAt(holderToMove, toPos);

        RemoveAt(fromPos, amount);
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

                if (emptySlots.Count != 0)
                {
                    MoveItem(i, emptySlots.Dequeue(), itemHolders[i].Amount);
                    emptySlots.Enqueue(i);
                }
            }
            catch (ArgumentException)
            {
                MoveItem(i, sortedItems[itemName], itemHolders[i].Amount);
            }
          
        }
    }
}
