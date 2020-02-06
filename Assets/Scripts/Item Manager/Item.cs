using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "RPG Generator/Items/Pure Item", order = 51)]
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
