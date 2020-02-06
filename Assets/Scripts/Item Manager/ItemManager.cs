using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemPossessor
    {
        BAG,
        P1,
        P2,
        P3,
        P4,
        P5,
        NONE
    }

    public const int MAX_INVENTORY_SIZE = 16;
    
    private int highestEntry = -1;
    public int HighestEntry { get { return highestEntry; } }

    private int numOfEntries;
    public int NumOfEntries { get { return numOfEntries; } set { numOfEntries = value; } }

    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } set { instance = value; } }

    [SerializeField]
    private Item nullItem = null;

    [SerializeField]
    private Item[] itemLibrary = null;

    private ItemHolder nullHolder = null;

    private ItemHolder[] itemHolders;
    public ItemHolder[] ItemHolders { get { return itemHolders; } }

    //private ItemHolder[][] compositeHolders;

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
        nullHolder = new ItemHolder(nullItem, -1, -1, ItemPossessor.NONE);
        itemHolders = new ItemHolder[MAX_INVENTORY_SIZE * (GameManager.NUM_OF_CHARS + 1)];
        //compositeHolders = new ItemHolder[GameManager.NUM_OF_CHARS + 1][];
    }

    private ItemHolder[] FillInvSlots(int size)
    {
        ItemHolder[] temp = new ItemHolder[size];
        for (int i = 0; i < size; i++) temp[i] = nullHolder;
        return temp;
    }

    private void TestItemData()
    {
        itemHolders = FillInvSlots(itemHolders.Length);

        itemHolders[0] = new ItemHolder(itemLibrary[2], 5, 0, ItemPossessor.BAG);
        itemHolders[1] = new ItemHolder(itemLibrary[0], 1, 3, ItemPossessor.BAG);
        itemHolders[2] = new ItemHolder(itemLibrary[1], 10, 6, ItemPossessor.BAG);
        itemHolders[3] = new ItemHolder(itemLibrary[4], 9, 7, ItemPossessor.BAG);
        itemHolders[4] = new ItemHolder(itemLibrary[3], 40, 9, ItemPossessor.BAG);
        itemHolders[5] = new ItemHolder(itemLibrary[5], 6, 15, ItemPossessor.BAG);
      
        numOfEntries = 6;
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

    public Item GetItemAt(int pos, ItemPossessor possessor)
    {
        for (int i = 0; i < numOfEntries; i++)
        {
            if (itemHolders[i].InvPosition == pos) return itemHolders[i].TheItem;
        }

        return null;
    }

    public ItemHolder[] GetInventory(ItemPossessor possessor)
    {
        ItemHolder[] temp = FillInvSlots(MAX_INVENTORY_SIZE);
        int currentPos = 0;

        for (int i = 0; i < numOfEntries; i++)
        {
            if (itemHolders[i].Possessor != possessor) continue;

            temp[currentPos] = itemHolders[i];
            currentPos++;
        }

        return temp;
    }

    public void AddItem(Item itemToAdd)
    {

    }

    private bool CheckExists(Item itemToCheck)
    {
        //for (int i = 0; i < )
    }
}
