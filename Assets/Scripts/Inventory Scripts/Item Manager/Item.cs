using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private string itemName = "", description = "";

    [SerializeField]
    private Sprite image = null;

    [SerializeField]
    private int sellValue = 0;

    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public Sprite Image { get { return image; } }
    public int SellValue { get { return sellValue; } }

    [SerializeField]
    private int changeToHP = 0;

    [SerializeField]
    private int changeToMP = 0;

    public void Use(CharStats charStats)
    {
        charStats.currentHP += changeToHP;
        charStats.currentMP += changeToMP;
    }

    public bool IsEqual(Item other)
    {
        return itemName == other.ItemName;
    }

}
