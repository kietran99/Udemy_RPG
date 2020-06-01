using UnityEngine;
using Inventory;

public class ItemSaleDisplay : MonoBehaviour, IInventoryDisplay
{
    [SerializeField]
    private GameObject invSlotPrototype = null, invOrganizer = null;

    private ItemButton[] itemButtons;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(SellMenuController controller, ItemHolder[] itemsToDisplay)
    {
        if (itemButtons != null) return;

        itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

        for (int i = 0; i < itemButtons.Length; i++)
        {
            GameObject btn = Instantiate(invSlotPrototype);
            itemButtons[i] = btn.GetComponent<ItemButton>();

            // Set position
            btn.transform.SetParent(invOrganizer.transform);

            // Set internal data
            itemButtons[i].Init((IClickInvoker)controller, i);
            itemButtons[i].DisplayItem(itemsToDisplay[i]);
        }
    }

    public void UpdateInv(ItemHolder[] holders)
    {      
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].DisplayItem(holders[i]);
        }
    }

    public void UpdateSlot(int slot, ItemHolder holder)
    {
        itemButtons[slot].DisplayItem(holder);
    }
}
