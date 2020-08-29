using System.Collections.Generic;
using UnityEngine;

public class BuyMenuController : TradeMenuController
{
    private const int MAX_BUYABLE = 99;

    [SerializeField]
    private MerchDisplay merchDisplay = null;

    [SerializeField]
    private GameObject buyButton = null;

    private List<Item> merchToDisplay;

    private Item selectedMerch;

    public void Activate(ShopDialog dialog, Item[] merchToDisplay)
    {
        this.dialog = dialog;
        SubscribeToDelegates();
        UpdateMerch(merchToDisplay);
        buyButton.SetActive(true);
        gameObject.SetActive(true);
        merchDisplay.Init(this, merchToDisplay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeInHierarchy) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        buyButton.SetActive(false);
    }

    private void UpdateMerch(Item[] merch)
    {
        if (merchToDisplay == null) merchToDisplay = new List<Item>();
        merchToDisplay.Clear();
        merchToDisplay.AddRange(merch);
    }

    public void Buy()
    {
        amtSelector.Activate(MAX_BUYABLE);
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public override void OnButtonClick(int pos)
    {
        selectedMerch = merchToDisplay[pos];
        if (selectedMerch is Equipment) merchDescription.UpdateDesc(selectedMerch as Equipment, selectedMerch.BuyValue);
        else merchDescription.UpdateDesc(selectedMerch/* as ConsumableItem*/, selectedMerch.BuyValue);
    }

    public override void OnAmountConfirm(int changeAmount)
    {
        int totalCost = changeAmount * selectedMerch.BuyValue;

        if (ItemManager.Instance.CurrentGold < totalCost)
        {
            dialog.InsufficientFund();
            return;
        }

        ItemManager.Instance.CurrentGold -= totalCost;

        int invAvail = ItemManager.Instance.AddItem(ItemPossessor.BAG, new ItemHolder(selectedMerch, changeAmount));
        if (invAvail == -1)
        {
            dialog.UnavailSlot();
            return;
        }

        if (changeAmount > 0)
        {
            dialog.TradeSuccessful();
        }
    }
}
