using UnityEngine;

public class DevQuest : MonoBehaviour
{
    public Item item;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Cheat: Add to inventory");
            RPG.Inventory.AbstractItemHolderFactory itemHolderFactory = new RPG.Inventory.ItemHolderFactory();
            int invAvail = ItemManager.Instance.AddItem(RPG.Inventory.InventoryOwner.BAG, 
                itemHolderFactory.CreateItemToObtainHolder(item));
            if (invAvail == Constants.INVALID) Debug.LogError("INVENTORY FULL!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            //Debug.Log("Cheat: Remove from inventory");
            
            //return;
        }       
    }
}
