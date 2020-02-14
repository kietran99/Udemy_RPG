using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryHolder
{
    private PossessorSearcher.ItemPossessor possessor;
    public PossessorSearcher.ItemPossessor Possessor { get { return possessor; } set { possessor = value; } }

    private ItemHolder[] itemHolders;
    public ItemHolder[] ItemHolders { get { return itemHolders; } set { itemHolders = value; } }

    public InventoryHolder(PossessorSearcher.ItemPossessor possessor, int size)
    {
        this.possessor = possessor;
        itemHolders = new ItemHolder[size];
    }

    public void Add(ItemHolder holder)
    {
        int pos = CheckExists(holder.TheItem);

        if (pos == -1)
        {
            itemHolders[itemHolders.Length] = holder;
        }
        else
        {
            itemHolders[pos].Amount += holder.Amount;
        }
    }

    public void Add(Item itemToAdd, int amount)
    {
        int pos = CheckExists(itemToAdd);
        ItemHolder holder = new ItemHolder(itemToAdd, amount, pos, possessor);

        // If item does not already exists in inventory then append it 
        if (pos == -1)
        {
            itemHolders[itemHolders.Length] = holder;
        }
        else // Increase the existing item by the wanted amount
        {
            itemHolders[pos].Amount += amount;
        }
    }

    private int CheckExists(Item itemToCheck)
    {
        for (int i = 0; i < itemHolders.Length; i++)
        {
            if (itemToCheck.IsEqual(itemHolders[i].TheItem)) return i;
        }

        return -1;
    }

    private int FindFirstEmptySlot()
    {
        for (int i = 0; i < itemHolders.Length; i++)
        {
            if (itemHolders[i].Possessor == PossessorSearcher.ItemPossessor.NONE) return i;
        }

        return -1;
    }

    public void Remove(Item itemToRemove, int amount)
    {
        int pos = CheckExists(itemToRemove);

        itemHolders[pos].Amount -= amount;
        if (itemHolders[pos].Amount <= 0) itemHolders = itemHolders.Where(x => x.Amount > 0).ToArray();
    }

    public void RemoveAt(int posToRemove, int amount)
    {
        itemHolders[posToRemove].Amount -= amount;
        if (itemHolders[posToRemove].Amount <= 0) itemHolders[posToRemove] = ItemHolder.NullHolder;
    }

    public void AddAt(ItemHolder itemToAdd, int posToAdd, int amount)
    {
        itemToAdd.Possessor = possessor;

        // If adding to an empty slot
        if (itemHolders[posToAdd].IsEmpty())
        {
            itemHolders[posToAdd] = itemToAdd;
            return;
        }

        // Else if adding to the same item
        itemHolders[posToAdd].Amount += amount;

        if (itemHolders[posToAdd].Amount > ItemHolder.ITEM_CAPACITY)
        {
            int exceededAmount = itemHolders[posToAdd].Amount - ItemHolder.ITEM_CAPACITY;
            itemHolders[posToAdd].Amount = ItemHolder.ITEM_CAPACITY;
            itemHolders[FindFirstEmptySlot()] = new ItemHolder(itemToAdd.TheItem, exceededAmount, possessor);
        }
    }
}
