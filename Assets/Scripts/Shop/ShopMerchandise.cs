using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMerchandise : MonoBehaviour
{
    #region
    public List<Equipment> Weapons { get { return weapons; } }
    public List<Equipment> Armours { get { return armours; } }
    public List<ConsumableItem> Consumables { get { return consumables; } }
    #endregion

    [SerializeField]
    private Item[] merchandise = null;

    private List<Equipment> weapons, armours;
    private List<ConsumableItem> consumables;

    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<Equipment>();
        armours = new List<Equipment>();
        consumables = new List<ConsumableItem>();
        FilterMerch();
    }
    
    private void FilterMerch()
    {
        if (merchandise.Length == 0) return;

        foreach (Item merch in merchandise)
        {          
            if (merch is ConsumableItem) consumables.Add(merch as ConsumableItem);
            else if (merch is Weapon) weapons.Add(merch as Equipment);
            else armours.Add(merch as Equipment);
        }
    }
}
