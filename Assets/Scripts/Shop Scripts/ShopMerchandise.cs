using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMerchandise : MonoBehaviour
{
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

    }
}
