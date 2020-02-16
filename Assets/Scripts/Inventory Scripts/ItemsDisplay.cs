using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDisplay : MonoBehaviour, AmountSelector.IEnabler
{
    [SerializeField]
    private Text itemNameText = null, itemDescriptionText = null, possessorText = null;
    public Text ItemNameText { get { return itemNameText; } }
    public Text ItemDescriptionText { get { return itemDescriptionText; } }

    [SerializeField]
    private Text primaryActionText;
    public Text PrimaryActionText { get { return primaryActionText; } }

    [SerializeField]
    private GameObject inventoryOrganizer = null, templateButton = null;

    [SerializeField]
    private GameObject itemInteractor = null, amountSelector = null, itemMovement = null;

    private PossessorSearcher.ItemPossessor currentPossessor;
    public PossessorSearcher.ItemPossessor CurrentPossessor { get { return currentPossessor; } }

    private ItemHolder[] currentInv;
    public ItemHolder[] CurrentInv { get { return currentInv; } }

    private ItemButton[] itemButtons;
    public ItemButton[] ItemButtons { get { return itemButtons; } }

    private int selectedPos;
    public int SelectedPos { get { return selectedPos; } set { selectedPos = value; } }

    private CircularLinkedList<PossessorSearcher.ItemPossessor> invList;

    // Inventory states
    private InventoryState defaultState, itemMoveState, discardState;
    private InventoryState currentState;

    // Start is called before the first frame update
    void Start()
    {
        InitPossessors(); 
        currentInv = ItemManager.Instance.GetInventory(currentPossessor);
        InitInvStates();
        InitButtonsGUI();
        DisplayAllItems();
    }

    void OnEnable()
    {
        currentPossessor = PossessorSearcher.ItemPossessor.BAG;
        if (itemButtons != null) DisplayAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPossessors()
    {
        selectedPos = -1;
        currentPossessor = PossessorSearcher.ItemPossessor.BAG;
        possessorText.text = PossessorSearcher.bagPossessor;
        invList = new CircularLinkedList<PossessorSearcher.ItemPossessor>();
        PossessorSearcher.FillPossessorList(invList);
    }

    private void InitInvStates()
    {
        defaultState = new DefaultState(this);
        itemMoveState = new ItemMoveState(this);
        discardState = new DiscardState(this);
        currentState = defaultState;
    }

    private void InitButtonsGUI()
    {
        GameObject temp = null;
        itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

        for (int i = 0; i < itemButtons.Length; i++)
        {
            temp = Instantiate(templateButton);
            temp.transform.SetParent(inventoryOrganizer.transform);
            itemButtons[i] = temp.GetComponent<ItemButton>();
            itemButtons[i].ButtonPos = i;
            int tempInt = i;
            itemButtons[i].GetComponent<Button>().onClick.AddListener(() => OnItemSelected(tempInt)); // lambdas expressions -> pass by ref
        }
    }

    public void DisplayAllItems()
    {
        SetItemInteractor(true, false, false);
        Reset();

        currentInv = ItemManager.Instance.GetInventory(currentPossessor);
        possessorText.text = PossessorSearcher.GetPossessor(currentPossessor);

        for (int i = 0; i < ItemManager.MAX_INVENTORY_SIZE; i++)
        {
            itemButtons[i].DisplayItem(currentInv[i].TheItem.Image, currentInv[i].Amount);
        }
    }

    private void Reset()
    {
        itemNameText.text = "";
        itemDescriptionText.text = "";
        selectedPos = -1;
    }

    public void Organize()
    {
        ItemManager.Instance.Organize(currentPossessor);
        DisplayAllItems();
    }

    public void OnItemSelected(int pos)
    {
        selectedPos = pos;
        currentState.OnItemSelected(pos);
    }

    public void DisplayItemDetails(int pos)
    {
        Item selectedItem = currentInv[pos].TheItem;

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

    public void NextPossessor()
    {
        string possText = "";
        do
        {
            invList.NextPos();
            possText = PossessorSearcher.GetPossessor(invList.current.value);            
        }
        while (possText.Equals(""));

        currentPossessor = invList.current.value;
        possessorText.text = possText;
        DisplayAllItems();
    }

    public void PrevPossessor()
    {
        string possText = "";
        do
        {
            invList.PrevPos();
            possText = PossessorSearcher.GetPossessor(invList.current.value);            
        }
        while (possText.Equals(""));

        currentPossessor = invList.current.value;
        possessorText.text = possText;
        DisplayAllItems();
    }

    public void Discard()
    {
        if (selectedPos < 0) return;

        currentState = discardState;
        EnableAmountSelector(currentInv[selectedPos].Amount);
    }

    public void EnableAmountSelector()
    {
        SetItemInteractor(false, true, false);
        amountSelector.GetComponent<AmountSelector>().SetEnabler(this, gameObject, currentInv[selectedPos].Amount);
    }

    public void EnableAmountSelector(int quantity)
    {
        SetItemInteractor(false, true, false);
        amountSelector.GetComponent<AmountSelector>().SetEnabler(this, gameObject, quantity);
    }

    public void OnAmountConfirm(int changeAmount)
    {       
        currentState.OnAmountConfirm(changeAmount);
        currentState = defaultState;
        DisplayAllItems();
    }

    public void ReduceItemAmount(int changeAmount)
    {
        ItemButton button = itemButtons[selectedPos];
        int amount = int.Parse(button.AmountText.text) - changeAmount;
        if (amount != 0) button.DisplayItem(button.ItemImage.sprite, amount);
        else
        {
            button.DisplayItem(null, -1);
            itemNameText.text = "";
            itemDescriptionText.text = "";
        }
    }

    public void Move()
    {
        if (selectedPos <= 0) return;

        (itemMoveState as ItemMoveState).SetItemToBeMoved(currentPossessor, selectedPos, currentInv[selectedPos].Amount);
        currentState = itemMoveState;
        SetItemInteractor(false, false, true);
    }

    public void SetItemInteractor(bool interactor, bool amtSelector, bool movement)
    {
        itemInteractor.SetActive(interactor);
        amountSelector.SetActive(amtSelector);
        itemMovement.SetActive(movement);
    }
}
