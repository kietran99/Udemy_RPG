using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public const int MAX_INVENTORY_SIZE = 16;
    
    private int highestEntry = -1;
    public int HighestEntry { get { return highestEntry; } }

    private int numOfEntries;
    public int NumOfEntries { get { return numOfEntries; } set { numOfEntries = value; } }

    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } set { instance = value; } }

    [SerializeField]
    private Item nullItem = null;
    public Item NullItem { get { return nullItem; } }

    [SerializeField]
    private Item[] itemLibrary = null;

    //private ItemHolder ItemHolder.NullHolder = null;

    private ItemHolder[] itemHolders;
    public ItemHolder[] ItemHolders { get { return itemHolders; } }

    private InventoryHolder[] invHolders;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitItemLibrary();
        TestItemData();
        SearchHighestEntry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitItemLibrary()
    {
        ItemHolder.InitNullHolder();
        itemHolders = new ItemHolder[MAX_INVENTORY_SIZE * (GameManager.NUM_OF_CHARS + 1)];

        int pos = 0;
        invHolders = new InventoryHolder[GameManager.NUM_OF_CHARS + 1];

        foreach (PossessorSearcher.ItemPossessor possessor in (PossessorSearcher.ItemPossessor[]) System.Enum.GetValues(typeof(PossessorSearcher.ItemPossessor)))
        {
            if (possessor == PossessorSearcher.ItemPossessor.NONE) continue;
            invHolders[pos] = new InventoryHolder(possessor, MAX_INVENTORY_SIZE);
            pos++;
        }
    }

    private void FillInvSlots(ItemHolder[] holders)
    {
        for (int i = 0; i < holders.Length; i++) holders[i] = ItemHolder.NullHolder;
    }

    private void TestItemData()
    {
        /*FillInvSlots(itemHolders);

        itemHolders[0] = new ItemHolder(itemLibrary[2], 5, 0, PossessorSearcher.ItemPossessor.BAG);
        itemHolders[1] = new ItemHolder(itemLibrary[0], 1, 3, PossessorSearcher.ItemPossessor.BAG);
        itemHolders[2] = new ItemHolder(itemLibrary[1], 10, 6, PossessorSearcher.ItemPossessor.BAG);
        itemHolders[3] = new ItemHolder(itemLibrary[4], 9, 7, PossessorSearcher.ItemPossessor.BAG);
        itemHolders[4] = new ItemHolder(itemLibrary[3], 40, 9, PossessorSearcher.ItemPossessor.BAG);
        itemHolders[5] = new ItemHolder(itemLibrary[5], 6, 15, PossessorSearcher.ItemPossessor.BAG);
      
        numOfEntries = 6;*/

        // Inventory holder test
        foreach (InventoryHolder myHolder in invHolders)
        {
            FillInvSlots(myHolder.ItemHolders);
        }

        InventoryHolder holder = invHolders[0];

        holder.ItemHolders[0] = new ItemHolder(itemLibrary[2], 5, 0, PossessorSearcher.ItemPossessor.BAG);
        holder.ItemHolders[3] = new ItemHolder(itemLibrary[0], 1, 3, PossessorSearcher.ItemPossessor.BAG);
        holder.ItemHolders[6] = new ItemHolder(itemLibrary[1], 10, 6, PossessorSearcher.ItemPossessor.BAG);
        holder.ItemHolders[7] = new ItemHolder(itemLibrary[4], 9, 7, PossessorSearcher.ItemPossessor.BAG);
        holder.ItemHolders[9] = new ItemHolder(itemLibrary[3], 40, 9, PossessorSearcher.ItemPossessor.BAG);
        holder.ItemHolders[15] = new ItemHolder(itemLibrary[5], 6, 15, PossessorSearcher.ItemPossessor.BAG);

        holder = invHolders[1];
        holder.ItemHolders[1] = new ItemHolder(itemLibrary[6], 1,PossessorSearcher.ItemPossessor.P1);
        holder.ItemHolders[4] = new ItemHolder(itemLibrary[7], 2, PossessorSearcher.ItemPossessor.P1);
        holder.ItemHolders[7] = new ItemHolder(itemLibrary[8], 1, PossessorSearcher.ItemPossessor.P1);
        holder.ItemHolders[8] = new ItemHolder(itemLibrary[9], 2, PossessorSearcher.ItemPossessor.P1);
        holder.ItemHolders[13] = new ItemHolder(itemLibrary[10], 1, PossessorSearcher.ItemPossessor.P1);
    }

    private void SearchHighestEntry()
    {
        for (int i = 0; i < numOfEntries; i++)
        {
            if (itemHolders[i].InvPosition > highestEntry) highestEntry = itemHolders[i].InvPosition;
        }
    }

    public void Organize()
    {
        Sort();

        for (int i = 0; i < numOfEntries; i++)
        {
            itemHolders[i].InvPosition = i;
        }

        //Reassign the highest entry after organization
        highestEntry = numOfEntries - 1;
    }

    private void Sort()
    {
        for (int i = 1; i < numOfEntries; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (itemHolders[i].InvPosition > itemHolders[i - 1].InvPosition) break;

                //Swap 2 values
                itemHolders[i - 1].InvPosition += itemHolders[i].InvPosition;

                itemHolders[i].InvPosition = itemHolders[i - 1].InvPosition - itemHolders[i].InvPosition;

                itemHolders[i - 1].InvPosition -= itemHolders[i].InvPosition;
            }
        }
    }

    public void Organize(PossessorSearcher.ItemPossessor possessor)
    {
        ItemHolder[] holders = GetInventory(possessor);

        for (int i = 0; i < holders.Length; i++)
        {
            if (holders[i].IsEmpty())
            {
                for (int j = i + 1; j < holders.Length; j++)
                {
                    if (holders[j].IsEmpty()) continue;

                    holders[i] = holders[j];
                    holders[j] = ItemHolder.NullHolder;
                    break;
                }
            }
        }
    }

    public Item GetItemAt(int pos, PossessorSearcher.ItemPossessor possessor)
    {
        ItemHolder[] inv = GetInventory(possessor);

        if (inv == null) return nullItem;

        return inv[pos].TheItem;
    }

    public ItemHolder[] GetInventory(PossessorSearcher.ItemPossessor possessor)
    {       
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) return holder.ItemHolders;
        }

        return null;
    }

    public void AddItem(PossessorSearcher.ItemPossessor possessor, Item itemToAdd, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.Add(itemToAdd, amount);
        }
    }

    public void AddItemAt(PossessorSearcher.ItemPossessor possessor, ItemHolder itemToAdd, int posToAdd, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.AddAt(itemToAdd, posToAdd, amount);
        }
    }

    public void RemoveItem(PossessorSearcher.ItemPossessor possessor, Item itemToRemove, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.Remove(itemToRemove, amount);
        }
    }

    public void RemoveItemAt(PossessorSearcher.ItemPossessor possessor, int posToRemove, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.RemoveAt(posToRemove, amount);
        }
    }
}
