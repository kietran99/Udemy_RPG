﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopActionDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject actionCanvas = null, itemTypeCanvas = null;

    [SerializeField]
    private Button[] actionButtons = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToggleActionCanvas(bool flag)
    {
        actionCanvas.SetActive(flag);
        ToggleActionButtons(flag);
    }

    public void ToggleItemTypeCanvas(bool flag)
    {
        itemTypeCanvas.SetActive(flag);
        ToggleActionButtons(!flag);
    }

    public void ToggleItemSellMenu(bool flag)
    {
        ToggleActionCanvas(!flag);
    }

    public void ToggleMenuDisplay(bool flag)
    {
        actionCanvas.SetActive(flag);
        itemTypeCanvas.SetActive(flag);
    }

    private void ToggleActionButtons(bool flag)
    {
        foreach (Button btn in actionButtons) btn.interactable = flag;
    }

}
