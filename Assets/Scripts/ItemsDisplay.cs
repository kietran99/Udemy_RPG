using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDisplay : MonoBehaviour
{
    [SerializeField]
    private Text itemNameText = null, itemDescriptionText = null, possessorText = null;

    [SerializeField]
    private GameObject inventoryOrganizer = null, templateButton = null;

    private ItemManager.ItemPossessor currentPossessor;

    private ItemButton[] itemButtons;

    // Start is called before the first frame update
    void Start()
    {
        currentPossessor = ItemManager.ItemPossessor.BAG;
        possessorText.text = "BAG";

        GameObject temp = null; 
        itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

        for(int i = 0; i < itemButtons.Length; i++)
        {
            temp = Instantiate(templateButton);
            temp.transform.SetParent(inventoryOrganizer.transform);
            itemButtons[i] = temp.GetComponent<ItemButton>();
            itemButtons[i].ButtonPos = i;
            int tempInt = i;
            itemButtons[i].GetComponent<Button>().onClick.AddListener(() => DisplayItemDetails(tempInt)); // lambdas expressions -> pass by ref
        }

        DisplayAllItems();
    }

    void OnEnable()
    {
        if (itemButtons != null) DisplayAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayAllItems()
    {
        ItemHolder[] itemHolders = ItemManager.Instance.ItemHolders;

        // Clear all buttons
        foreach (ItemButton button in itemButtons) button.ItemMark = false;

        // Display non-empty slots
        for (int i = 0; i < ItemManager.Instance.NumOfEntries; i++)
        {
            if (itemHolders[i].Amount <= 0) break;

            itemButtons[itemHolders[i].InvPosition].DisplayItem(itemHolders[i].TheItem.Image, itemHolders[i].Amount);
            itemButtons[itemHolders[i].InvPosition].ItemMark = true;
        }

        // Display empty slots
        foreach (ItemButton button in itemButtons)
        {
            if (!button.ItemMark) button.DisplayItem(null, -1);
        }
       
    }

    public void Organize()
    {
        ItemManager.Instance.Organize();
        DisplayAllItems();
    }

    public void DisplayItemDetails(int pos)
    {
        Item selectedItem = ItemManager.Instance.GetItemAt(pos, currentPossessor);

        if (selectedItem != null)
        {
            itemNameText.text = selectedItem.ItemName;
            itemDescriptionText.text = selectedItem.Description;
        }
        else
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
        }       
    }

    public void SwitchPossessor()
    {

    }

}
