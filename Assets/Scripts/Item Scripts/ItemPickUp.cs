﻿using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private Item itemToPickUp = null;

    private bool canPickUp;

    private AbstractItemHolderFactory itemHolderFactory;

    // Start is called before the first frame update
    void Start()
    {
        itemHolderFactory = new ItemHolderFactory();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetButtonDown("Fire1") && PlayerController.Instance.canMove)
        {
            int invAvail = ItemManager.Instance.AddItem(PossessorSearcher.ItemPossessor.BAG, itemHolderFactory.CreateItemToObtainHolder(itemToPickUp));
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
