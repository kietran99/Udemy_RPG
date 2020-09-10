using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private Item itemToPickUp = null;

    private bool canPickUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetButtonDown("Fire1") && PlayerController.Instance.canMove)
        {
            AbstractItemHolderFactory itemHolderFactory = new ItemHolderFactory();
            int invAvail = ItemManager.Instance.AddItem(ItemOwner.BAG, itemHolderFactory.CreateItemToObtainHolder(itemToPickUp));
            if (invAvail == -1) Debug.LogError("INVENTORY FULL!");
            else Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canPickUp = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canPickUp = false;
    }
}
