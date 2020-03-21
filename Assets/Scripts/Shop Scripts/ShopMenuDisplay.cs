using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuDisplay : UIDisplay
{
    [SerializeField]
    private GameObject actionPanel = null, itemTypePanel = null, merchandisePanel = null, dialogCanvas = null;

    [SerializeField]
    private Button[] actionButtons = null;

    public override void DisplayAll()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActionPanel(bool flag)
    {
        actionPanel.SetActive(flag);
        ToggleActionButtons(flag);
    }

    public void ToggleItemTypePanel(bool flag)
    {
        itemTypePanel.SetActive(flag);
        ToggleActionButtons(!flag);
    }

    public void ToggleItemSellMenu(bool flag)
    {
        ToggleActionPanel(!flag);
    }

    public void ToggleShopDialog(bool flag)
    {
        dialogCanvas.SetActive(flag);
    }

    private void ToggleActionButtons(bool flag)
    {
        foreach (Button btn in actionButtons) btn.interactable = flag;
    }
}
