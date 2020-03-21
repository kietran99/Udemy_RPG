using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField]
    private ShopMenuDisplay display = null;

    [SerializeField]
    private ShopMerchandise merchandise = null;

    [SerializeField]
    private ShopDialog dialog = null;

    // Start is called before the first frame update
    void Start()
    {
        display.ToggleActionPanel(true);
        display.ToggleShopDialog(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem()
    {
        display.ToggleItemTypePanel(true);
    }

    public void SellItem()
    {
        display.ToggleItemSellMenu(true);
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
        GameManager.Instance.shopMenuActive = false;
    }

    public void BuyWeapons()
    {

    }

    public void BuyArmours()
    {

    }

    public void BuyConsumables()
    {

    }

    public void CancelItemType()
    {

    }
}
