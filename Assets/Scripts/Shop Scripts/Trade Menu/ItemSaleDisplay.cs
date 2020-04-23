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

        ItemHolder holder;

        itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

        for (int i = 0; i < itemButtons.Length; i++)
        {
            GameObject btn = Instantiate(invSlotPrototype);
            itemButtons[i] = btn.GetComponent<ItemButton>();

            // Set position
            btn.transform.SetParent(invOrganizer.transform);

            // Set internal data
            itemButtons[i].Init((IClickInvoker)controller, i);
            holder = itemsToDisplay[i];
            itemButtons[i].DisplayItem(holder.TheItem.Image, holder.Amount, holder.IsEquipped);
        }

        UpdateInv(itemsToDisplay);
    }

    public void UpdateInv(ItemHolder[] items)
    {
        ItemHolder holder;

        for (int i = 0; i < itemButtons.Length; i++)
        {
            holder = items[i];
            itemButtons[i].DisplayItem(holder.TheItem.Image, holder.Amount, holder.IsEquipped);
        }
    }

    public void UpdateSlot(int slot, ItemHolder item)
    {
        itemButtons[slot].DisplayItem(item.TheItem.Image, item.Amount, item.IsEquipped);
    }
}
