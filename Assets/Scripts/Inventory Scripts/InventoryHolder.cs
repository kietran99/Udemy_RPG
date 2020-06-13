using System;
using System.Collections.Generic;
using UnityEngine;
public class InventoryHolder: IInventoryHolder
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
            if (itemToCheck.TheItem.Equals(itemHolders[i].TheItem)) return i;
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
        }
        else 
        {
            AddToExistingItem(itemHolderToAdd, posToAdd);
        }
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
        return Functional.HigherOrderFunc.Filter(x => x.IsEquipped, itemHolders);
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

    public void MoveItem(int fromPos, int toPos, int amount, IInventoryHolder toHolder = null)
    {
        if (amount <= 0 || itemHolders[fromPos].IsEmpty()) return;

        int amountToMove = amount, destAmount = itemHolders[toPos].Amount;

        if (destAmount + amount > ItemHolder.ITEM_CAPACITY) amountToMove = ItemHolder.ITEM_CAPACITY - destAmount;

        ItemHolder holderToMove;

        if (itemHolders[fromPos].IsEquipped)
        {
            holderToMove = new ItemHolderFactory().CreateEquipmentHolder(itemHolders[fromPos].TheItem);
        }
        else
        {
            holderToMove = new ItemHolderFactory().CreateRegularHolder(itemHolders[fromPos].TheItem, amountToMove);
        }

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
