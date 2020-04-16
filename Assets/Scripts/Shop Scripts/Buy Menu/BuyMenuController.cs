using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuController : IMenuController, IAmountConfirmable
{
    private const int MAX_BUYABLE = 99;

    private ShopMenuController shopMenu;

    [SerializeField]
    private MerchDisplay merchDisplay = null;

    [SerializeField]
    private MerchDescription merchDescription = null;

    [SerializeField]
    private AmountSelector amtSelector = null;

    [SerializeField]
    private GameObject defaultActions = null;

    private List<Item> merchToDisplay;

    private Item selectedMerch;

    public void Activate(ShopMenuController shopMenu, Item[] merchToDisplay)
    {
        this.shopMenu = shopMenu;
        UpdateMerch(merchToDisplay);
        gameObject.SetActive(true);
        merchDisplay.Init(this, merchToDisplay);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeInHierarchy) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        shopMenu.CloseMerchMenu();
    }

    private void UpdateMerch(Item[] merch)
    {
        if (merchToDisplay == null) merchToDisplay = new List<Item>();
        merchToDisplay.AddRange(merch);
    }

    public void Buy()
    {
        amtSelector.Activate((IAmountConfirmable) this, merchDisplay.gameObject, defaultActions, (ILiveAmountObserver) merchDescription, MAX_BUYABLE);
    }

    public void Exit()
    {
        Reset();
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        merchToDisplay.Clear();
    }

    public override void OnMerchClick(int pos)
    {
        selectedMerch = merchToDisplay[pos];
        if (selectedMerch is Equipment) merchDescription.UpdateDesc(selectedMerch as Equipment);
        else merchDescription.UpdateDesc(selectedMerch as ConsumableItem);
    }

    public void OnAmountConfirm(int changeAmount)
    {
        if (ItemManager.Instance.CurrentGold < changeAmount * selectedMerch.BuyValue)
        {
            Debug.Log("NOT ENOUGH GOLD!");
            return;
        }

        int invAvail = ItemManager.Instance.AddItem(PossessorSearcher.ItemPossessor.BAG, new ItemHolder(selectedMerch, changeAmount));
        if (invAvail == -1)
        {
            Debug.Log("INVENTORY FULL!!");
        }
    }
}
