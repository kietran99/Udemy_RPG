﻿using UnityEngine;
using RPG.Inventory;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private Item itemToPickUp = null;

    private bool canPickUp;
   
    void Update()
    {
        if (canPickUp && Input.GetButtonDown("Fire1") && PlayerController.Instance.canMove)
        {
            AbstractItemHolderFactory itemHolderFactory = new ItemHolderFactory();
            int invAvail = ItemManager.Instance.AddItem(ItemOwner.BAG, itemHolderFactory.CreateItemToObtainHolder(itemToPickUp));
            if (invAvail == Constants.INVALID) Debug.LogError("INVENTORY FULL!");
            else Destroy(gameObject);
        }
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
