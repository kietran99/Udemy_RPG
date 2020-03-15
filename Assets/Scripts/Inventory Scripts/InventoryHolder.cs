﻿using System.Linq;

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

    public void Add(ItemHolder itemHolderToAdd)
    {
        int posToCheck = CheckExists(itemHolderToAdd);
        if (posToCheck > -1) AddToExistingItem(itemHolderToAdd, posToCheck);
        else itemHolders[FindFirstEmptySlot()] = itemHolderToAdd;
    }

    public void AddAt(ItemHolder itemHolderToAdd, int posToAdd)
    {
        // If adding to an empty slot
        if (itemHolders[posToAdd].IsEmpty())
        {
            itemHolders[posToAdd] = itemHolderToAdd;
            return;
        }

        // Else adding to the same item
        AddToExistingItem(itemHolderToAdd, posToAdd);
    }

    private void AddToExistingItem(ItemHolder itemHolderToAdd, int posToAdd)
    {
        itemHolders[posToAdd].Amount += itemHolderToAdd.Amount;

        if (itemHolders[posToAdd].Amount > ItemHolder.ITEM_CAPACITY)
        {
            itemHolders[posToAdd].Amount = ItemHolder.ITEM_CAPACITY;
            itemHolders[FindFirstEmptySlot()] = new ItemHolder(itemHolderToAdd.TheItem, itemHolders[posToAdd].Amount - ItemHolder.ITEM_CAPACITY);
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

    // Only for toggling equip ability
    public void MoveEquipment(int fromPos, int toPos)
    {       
        AddAt(new ItemHolderFactory().CreateEquipmentHolder(itemHolders[fromPos].TheItem), toPos);
        RemoveAt(fromPos, 1);
    }

    public void UseItem(int pos, CharStats charToUse)
    {
        itemHolders[pos].Use(charToUse);
        RemoveAt(pos, 1);
    }   
}