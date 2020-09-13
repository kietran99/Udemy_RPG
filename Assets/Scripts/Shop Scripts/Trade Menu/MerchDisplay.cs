using System.Collections.Generic;
using UnityEngine;

public class MerchDisplay : MonoBehaviour
{    
    [SerializeField]
    private GameObject organizer = null, merchInfoPrefab = null;

    private List<GameObject> merchInfoButtons;   
   
    public void Init(TradeMenuController controller, Item[] merchToDisplay)
    {
        if (merchInfoButtons == null) merchInfoButtons = new List<GameObject>();
        else Reset();

        for (int i = 0; i < merchToDisplay.Length; i++)
        {
            GameObject btn = Instantiate(merchInfoPrefab, organizer.transform);
            merchInfoButtons.Add(btn); 
            
            Item currentItem = merchToDisplay[i];
            btn.GetComponent<MerchInfo>().SetData((IClickObserve) controller, currentItem.Image, currentItem.ItemName, currentItem.BuyValue, i);
        }
    }

    private void Reset()
    {
        foreach (GameObject obj in merchInfoButtons) Destroy(obj);
        merchInfoButtons.Clear();
    }
}
