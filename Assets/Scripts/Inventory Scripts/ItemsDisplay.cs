using RPG.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDisplay : UIDisplay, IClickObserve
{  
    #region
    public Text ItemNameText { get { return itemNameText; } }
    public Text ItemDescriptionText { get { return itemDescriptionText; } }
    public Text PrimaryActionText { get { return primaryActionText; } }
    public ItemOwner CurrentPossessor { get { return currentPossessor; } }
    public ItemHolder[] CurrentInv { get { return currentInv; } }
    public int SelectedPos { get { return selectedPos; } set { selectedPos = value; } }
    #endregion

    #region skip
    [SerializeField]
    private Text itemNameText = null, itemDescriptionText = null, possessorText = null;

    [SerializeField]
    private Text primaryActionText = null;

    [SerializeField]
    private GameObject inventoryOrganizer = null, templateButton = null;

    [SerializeField]
    private GameObject itemInteractor = null, amountSelector = null, itemMovement = null, userChooser = null;

    [SerializeField]
    private Button primaryActionButton = null;
    #endregion

    private PrimaryActionInvoker primInvoker;

    #region skip
    private ItemOwner currentPossessor;

    private ItemHolder[] currentInv;

    private ItemButton[] itemButtons;

    private int selectedPos;

    private CircularLinkedList<ItemOwner> invList;

    private InventoryState defaultState, itemMoveState, discardState, itemUseState;
    private InventoryState currentState;

    // Start is called before the first frame update
    void Start()
    {
        InitEssentials(); 
        InitInvStates();
        InitItemButtons();
        DisplayAll(); 
    }

    void OnEnable()
    {
        DisableInteractors();
        EnableButtons();
        currentPossessor = ItemOwner.BAG;
        currentState = defaultState;
        if (invList != null) invList.RevertToDefault();
        if (itemButtons != null) DisplayAll();
    }

    private void DisableInteractors()
    {
        amountSelector.SetActive(false);
        userChooser.SetActive(false);
        itemMovement.SetActive(false);
    }

    private void EnableButtons()
    {
        foreach (Button button in gameObject.GetComponentsInChildren<Button>()) button.interactable = true;
        foreach (Button button in itemInteractor.gameObject.GetComponentsInChildren<Button>()) button.interactable = true;
    }

    private void InitEssentials()
    {
        selectedPos = -1;
        currentPossessor = ItemOwner.BAG;
        possessorText.text = PossessorSearcher.bagPossessor;
        invList = new CircularLinkedList<ItemOwner>();
        PossessorSearcher.FillPossessorList(invList);
        primInvoker = new PrimaryActionInvoker(this);
    }

    private void InitInvStates()
    {
        defaultState    = new DefaultState(this);
        itemMoveState   = new ItemMoveState(this);
        discardState    = new DiscardState(this);
        itemUseState    = new ItemUseState();
        currentState    = defaultState;
    }

    private void InitItemButtons()
    {
        GameObject temp = null;
        itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

        for (int i = 0; i < itemButtons.Length; i++)
        {
            temp = Instantiate(templateButton);
            temp.transform.SetParent(inventoryOrganizer.transform);
            itemButtons[i] = temp.GetComponent<ItemButton>();
            itemButtons[i].Init((IClickObserve) this, i);
        }
    }

    public override void DisplayAll()
    {
        if (currentState == defaultState) Reset();

        currentInv = ItemManager.Instance.GetInventory(currentPossessor);
        possessorText.text = PossessorSearcher.GetPossessorName(currentPossessor);

        for (int i = 0; i < ItemManager.MAX_INVENTORY_SIZE; i++)
        {
            itemButtons[i].DisplayItem(currentInv[i]);
        }
    }

    private void Reset()
    {        
        itemInteractor.SetActive(true);
        selectedPos = -1;
        itemNameText.text = "";
        itemDescriptionText.text = "";       
        primaryActionText.text = "USE";
    }

    public void ToDefaultState()
    {
        currentState = defaultState;
    }
    public void Organize()
    {
        ItemManager.Instance.GetInvHolder(currentPossessor).Organize();
        DisplayAll();
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
            possText = PossessorSearcher.GetPossessorName(invList.current.value);            
        }
        while (possText.Equals(""));

        currentPossessor = invList.current.value;
        possessorText.text = possText;
        DisplayAll();
    }

    public void PrevPossessor()
    {
        string possText = "";
        do
        {
            invList.PrevPos();
            possText = PossessorSearcher.GetPossessorName(invList.current.value);            
        }
        while (possText.Equals(""));

        currentPossessor = invList.current.value;
        possessorText.text = possText;
        DisplayAll();
    }

    public void Discard()
    {
        if (IsEmptySlot()) return;

        currentState = discardState;
        EnableAmountSelector(currentInv[selectedPos].Amount);
    }

    public void EnableAmountSelector(int quantity)
    {
        amountSelector.GetComponent<AmountSelector>().Activate(quantity);
    }

    public void OnAmountConfirm(int changeAmount)
    {       
        currentState.OnAmountConfirm(changeAmount);
        currentState = defaultState;
        DisplayAll();
    }

    public void Move()
    {
        if (IsEmptySlot()) return;

        currentState = itemMoveState;
        (itemMoveState as ItemMoveState).Activate(itemInteractor, itemMovement, currentPossessor, selectedPos, currentInv[selectedPos].Amount);        
    }
    #endregion

    public void InvokePrimaryAction()
    {
        if (IsEmptySlot()) return;

        if (primaryActionText.text.Equals("USE"))
        {
            currentState = itemUseState;
            primInvoker.UseItem(itemInteractor, userChooser.GetComponent<UserChooser>());
        }
        else
        {
            ToggleEquipAbility();
        }
    }
    
    private void ToggleEquipAbility()
    {
        if (currentPossessor == ItemOwner.BAG) return;

        primInvoker.ToggleEquipAbility(); 
    }

    private bool SetPrimaryButtonInteractable()
    {
        if (currentInv[selectedPos].IsEmpty()) return false;

        if (currentPossessor == ItemOwner.BAG)
        {
            if (!primaryActionText.text.Equals(Item.USE_ACTION))
            {
                return false;
            }
            return true;
        }

        if (primaryActionText.text.Equals(Item.USE_ACTION)) return true;

        if (((ItemManager.Instance.GetItemAt(selectedPos, currentPossessor)) as Equipment).CanEquip(PossessorSearcher.GetPossessorName(currentPossessor))) return true;

        return false;
    }

    #region skip
    private bool IsEmptySlot()
    {
        if (selectedPos < 0) return true;

        if (currentInv[selectedPos].IsEmpty()) return true;

        return false;
    }

    public void OnButtonClick(int val)
    {
        selectedPos = val;
        currentState.OnItemSelected(val);
        if (currentState == defaultState) primaryActionButton.interactable = SetPrimaryButtonInteractable();
    }
    #endregion
}
