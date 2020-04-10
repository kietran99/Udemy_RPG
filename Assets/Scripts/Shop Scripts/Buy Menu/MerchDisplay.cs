using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchDisplay : MonoBehaviour
{
    [SerializeField]
    private float firstX = 0f, firstY = 0f, offset = 130f;

    [SerializeField]
    private GameObject merchInfoPrototype = null, merchPanel = null;

    private BuyMenuController controller;

    private List<GameObject> merchInfoButtons;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(BuyMenuController controller, Item[] merchToDisplay)
    {
        if (merchInfoButtons == null) merchInfoButtons = new List<GameObject>();
        else Reset();

        for (int i = 0; i < merchToDisplay.Length; i++)
        {
            GameObject btn = Instantiate(merchInfoPrototype, merchPanel.transform);
            merchInfoButtons.Add(btn); 

            // Set position
            btn.transform.SetParent(merchPanel.transform);
            if (i != 0) btn.transform.position = merchInfoButtons[i - 1].transform.position - new Vector3(0f, offset, 0f);
            else btn.transform.position = new Vector3(firstX, firstY, 0f);
            
            // Set internal data
            Item currentItem = merchToDisplay[i];
            btn.GetComponent<MerchInfo>().SetData((MerchInfo.IClickable) controller, currentItem.Image, currentItem.ItemName, currentItem.BuyValue, i);
        }
    }

    private void Reset()
    {
        foreach (GameObject obj in merchInfoButtons) Destroy(obj);
        merchInfoButtons.Clear();
    }
}
