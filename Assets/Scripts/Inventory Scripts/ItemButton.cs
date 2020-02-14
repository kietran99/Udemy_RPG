using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{   
    [SerializeField]
    private Image itemImage = null;
    public Image ItemImage { get { return itemImage; } set { itemImage = value; } }

    [SerializeField]
    private Text amountText = null;
    public Text AmountText { get { return amountText; } set { amountText = value; } }

    private int buttonPos;
    public int ButtonPos { get { return buttonPos; } set { buttonPos = value; } }

    private bool itemMark = false;
    public bool ItemMark { get { return itemMark; } set { itemMark = value; } }

    void Start()
    {
        
    }

    public void DisplayItem(Sprite itemSprite, int amount)
    {
        if (itemSprite == null)
        {
            itemImage.enabled = false;
            amountText.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
            amountText.enabled = true;
            itemImage.sprite = itemSprite;
            amountText.text = amount.ToString();
        }
    }

}
