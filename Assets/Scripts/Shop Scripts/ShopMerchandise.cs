using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMerchandise : MonoBehaviour
{
    #region
    public List<Item> Weapons { get { return weapons; } }
    public List<Item> Armours { get { return armours; } }
    public List<Item> Consumables { get { return consumables; } }
    #endregion

    [SerializeField]
    private Item[] merchandise = null;

    private List<Item> weapons, armours, consumables;

    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<Item>();
        armours = new List<Item>();
        consumables = new List<Item>();
        FilterMerch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FilterMerch()
    {
        if (merchandise.Length == 0) return;

        foreach (Item merch in merchandise)
        {          
            if (merch is ConsumableItem) consumables.Add(merch);
            else if (merch is Weapon) weapons.Add(merch);
            else armours.Add(merch);
        }
    }
}
