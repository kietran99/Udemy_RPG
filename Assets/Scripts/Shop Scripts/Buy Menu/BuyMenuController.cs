using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuController : IMenuController
{
    private ShopMenuController shopMenu;

    [SerializeField]
    private MerchDisplay merchDisplay = null;

    [SerializeField]
    private MerchDescription merchDescription = null;

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

    }

    public void Cancel()
    {
        Reset();
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        merchToDisplay.Clear();
    }

    public override void OnClick(int pos)
    {
        selectedMerch = merchToDisplay[pos];
        merchDescription.UpdateDesc(selectedMerch as Equipment);
    }
}
