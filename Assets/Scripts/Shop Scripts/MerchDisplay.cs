using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchDisplay : UIDisplay, MerchInfo.IClickable
{
    [SerializeField]
    private float firstX = 0f, firstY = 0f, offset = 130f;

    [SerializeField]
    private GameObject merchInfoPrototype = null, statChangePrototype = null, merchPanel = null;

    [SerializeField]
    private MerchDescription description;

    private List<GameObject> merchInfoButtons;

    private List<Item> merchToDisplay;

    public override void DisplayAll()
    {
        
    }

    public void Activate(Item[] merchToDisplay)
    {
        UpdateMerch(merchToDisplay);
        UpdateMerchBtns();
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateMerch(Item[] merch)
    {
        if (merchToDisplay == null) merchToDisplay = new List<Item>();
        else merchToDisplay.Clear();
        merchToDisplay.AddRange(merch);
    }

    private void UpdateMerchBtns()
    {
        if (merchInfoButtons == null) merchInfoButtons = new List<GameObject>();
        else merchInfoButtons.Clear();

        for (int i = 0; i < merchToDisplay.Count; i++)
        {
            GameObject btn = Instantiate(merchInfoPrototype, merchPanel.transform);
            merchInfoButtons.Add(btn); 

            // Set position
            btn.transform.SetParent(merchPanel.transform);
            if (i != 0) btn.transform.position = merchInfoButtons[i - 1].transform.position - new Vector3(0f, offset, 0f);
            else btn.transform.position = new Vector3(firstX, firstY, 0f);
            
            // Set internal data
            Item currentItem = merchToDisplay[i];
            btn.GetComponent<MerchInfo>().SetData(this, currentItem.Image, currentItem.ItemName, currentItem.BuyValue, i);
        }
    }

    public void OnClick(int pos)
    {
        description.UpdateDesc(merchToDisplay[pos]);
    }
}
