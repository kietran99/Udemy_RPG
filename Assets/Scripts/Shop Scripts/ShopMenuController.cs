using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField]
    private ShopActionDisplay actionDisplay = null;

    [SerializeField]
    private BuyMenuController buyMenuController = null;

    [SerializeField]
    private ShopMerchandise merchandise = null;

    [SerializeField]
    private ShopDialog dialog = null;

    // Start is called before the first frame update
    void Start()
    {
        actionDisplay.ToggleActionCanvas(true);
        //actionDisplay.ToggleShopDialog(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem()
    {
        actionDisplay.ToggleItemTypeCanvas(true);
    }

    public void SellItem()
    {
        actionDisplay.ToggleItemSellMenu(true);
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
        GameManager.Instance.shopMenuActive = false;
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
        buyMenuController.Activate(this, merchToDisplay);        
    }

    public void CloseMerchMenu()
    {
        actionDisplay.ToggleMenuDisplay(true);
    }
}
