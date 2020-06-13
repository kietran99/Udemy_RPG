using UnityEngine;
using RPG.Inventory;

public class SellMenuController : TradeMenuController
{
    [SerializeField]
    private ItemSaleDisplay itemDisplay = null;

    [SerializeField]
    private InventoryController invController = null;

    [SerializeField]
    private GameObject sellButton = null;

    private int selectedPos;

    public void Activate(ShopDialog dialog)
    {
        this.dialog = dialog;
        sellButton.SetActive(true);
        gameObject.SetActive(true);
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

    private void OnEnable()
    {
        invController.Activate(itemDisplay);
        itemDisplay.Init(this, invController.CurrentInv);
    }

    private void OnDisable()
    {
        sellButton.SetActive(false);
    }

    public void Sell()
    {
        amtSelector.Activate(   (IAmountConfirmable)this, 
                                itemDisplay.gameObject, 
                                defaultActions, 
                                (ILiveAmountObserver)merchDescription, 
                                invController.CurrentInv[selectedPos].Amount);
    }

    public override void OnAmountConfirm(int changeAmount)
    {
        if (changeAmount <= 0) return;

        int totalSellValue = invController.CurrentInv[selectedPos].TheItem.SellValue;
        ItemManager.Instance.RemoveItemAt(invController.CharCycler.CurrPos, selectedPos, changeAmount);
        ItemManager.Instance.CurrentGold += totalSellValue * changeAmount;
        itemDisplay.UpdateSlot(selectedPos, invController.CurrentInv[selectedPos]);
        dialog.TradeSuccessful();
    }

    public override void OnInvokeeClick(int pos)
    {
        selectedPos = pos;
        ItemHolder selectedItem = invController.CurrentInv[pos];
        if (selectedItem.TheItem is Equipment) merchDescription.UpdateDesc(selectedItem.TheItem as Equipment, selectedItem.TheItem.SellValue);
        else merchDescription.UpdateDesc(selectedItem.TheItem, selectedItem.TheItem.SellValue);
    }

}
