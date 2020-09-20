using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTypeIconFetcher : MonoBehaviour
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Sprite consumablesSprite = null, armorSprite = null, headgearSprite = null, footwearSprite = null, 
        shieldSprite = null, swordSprite = null;

    private Dictionary<System.Type, Sprite> iconDict;

    void Start()
    {
        iconDict = new Dictionary<System.Type, Sprite>();
        AddEntry(typeof(ConsumableItem), consumablesSprite);
        AddEntry(typeof(Armor), armorSprite);
        AddEntry(typeof(Headgear), headgearSprite);
        AddEntry(typeof(Footwear), footwearSprite);
        AddEntry(typeof(Shield), shieldSprite);
        AddEntry(typeof(Sword), swordSprite);   
    }

    private void AddEntry(System.Type type, Sprite sprite) => iconDict[type] = sprite;

    public void Show(System.Type type)
    {
        icon.gameObject.SetActive(true);
        Sprite sprite;
        if (iconDict.TryGetValue(type, out sprite))
        {
            icon.sprite = sprite;
            return;
        }

        Hide();
    }

    public void Hide()
    {
        icon.gameObject.SetActive(false);
    }
}
