using UnityEngine;

public class TradeMenuToggler : MonoBehaviour
{
    [SerializeField]
    private BuyMenuController buyMenuController = null;

    [SerializeField]
    private SellMenuController sellMenuController = null;

    [SerializeField]
    private GameObject buyMenu = null, sellMenu = null;

    private ShopMenuController shopMenu;

    private ShopDialog dialog;
    
    private void OnDisable()
    {
        dialog.gameObject.SetActive(false);
        buyMenu.SetActive(false);
        sellMenu.SetActive(false);
        shopMenu.CloseTradeMenu();
    }

    public void ActivateBuyMenu(ShopMenuController shopMenu, ShopDialog dialog, Item[] merchToDisplay)
    {
        this.shopMenu = shopMenu;
        this.dialog = dialog;
        buyMenu.SetActive(true);
        buyMenuController.Activate(dialog, merchToDisplay);
        gameObject.SetActive(true);
    }

    public void ActivateSellMenu(ShopMenuController shopMenu, ShopDialog dialog)
    {
        this.shopMenu = shopMenu;
        this.dialog = dialog;
        sellMenu.SetActive(true);
        sellMenuController.Activate(dialog);
        gameObject.SetActive(true);
    }
}
