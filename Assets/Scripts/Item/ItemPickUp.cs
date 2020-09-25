using UnityEngine;
using RPG.Inventory;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private Item itemToPickUp = null;

    private bool canPickUp;
   
    void Update()
    {
        if (!canPickUp || !Input.GetKeyDown(KeyCode.Space) || !PlayerController.Instance.canMove) return;

        AttemptToObtain();
    }

    private void AttemptToObtain()
    {
        AbstractItemHolderFactory itemHolderFactory = new ItemHolderFactory();
        int invAvail = ItemManager.Instance.AddItem(InventoryOwner.BAG, itemHolderFactory.CreateItemToObtainHolder(itemToPickUp));
        if (invAvail == Constants.INVALID) Debug.LogError("INVENTORY FULL!");
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG)) canPickUp = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG)) canPickUp = false;
    }
}
