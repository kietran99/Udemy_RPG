using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField]
    private ShopActionDisplay actionDisplay = null;

    [SerializeField]
    private TradeMenuToggler tradeMenuToggler = null;

    [SerializeField]
    private ShopMerchandise merchandise = null;

    [SerializeField]
    private ShopDialog dialog = null;
    
    private void OnEnable()
    {
        actionDisplay.ToggleActionCanvas(true, dialog);
        dialog.Greetings();
    }

    public void BuyItem()
    {
        dialog.Buy();
        actionDisplay.ToggleItemTypeCanvas(true);        
    }

    public void SellItem()
    {
        actionDisplay.ToggleActionCanvas(false, null);
        dialog.Disable();
        tradeMenuToggler.ActivateSellMenu(this, dialog);
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
        dialog.Disable();
        GameManager.Instance.ShopMenuActive = false;
    }

    public void BuyWeapons()
    {
        OpenMerchMenu(merchandise.Weapons.ToArray());
    }

    public void BuyArmours()
    {
        OpenMerchMenu(merchandise.Armours.ToArray());
    }

    public void BuyConsumables()
    {
        OpenMerchMenu(merchandise.Consumables.ToArray());
    }
    
    private void OpenMerchMenu(Item[] merchToDisplay)
    {
        actionDisplay.ToggleMenuDisplay(false);
        dialog.Disable();
        tradeMenuToggler.ActivateBuyMenu(this, dialog, merchToDisplay);     
    }

    public void CloseTradeMenu()
    {
        actionDisplay.ToggleMenuDisplay(true);
        actionDisplay.ToggleItemTypeCanvas(false);
    }
}
