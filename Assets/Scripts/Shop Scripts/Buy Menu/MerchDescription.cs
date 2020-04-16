using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchDescription : MonoBehaviour, ILiveAmountObserver
{
    [SerializeField]
    private Text changeStatText = null;

    [SerializeField]
    private GameObject statChangePrototype = null;

    [SerializeField]
    private GameObject statChangeLayout = null;

    private CharStatChange[] statChanges;

    private int buyValue;

    [SerializeField]
    private Text totalCostText = null;

    [SerializeField]
    private Text currentGoldText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        InitStatChangeList();
    }

    private void InitStatChangeList()
    {
        if (statChanges != null) Reset();
            
        statChanges = new CharStatChange[GameManager.Instance.GetNumActives()];
        
        for (int i = 0; i < statChanges.Length; i++)
        {
            GameObject panel = Instantiate(statChangePrototype);
            panel.SetActive(false);
            panel.transform.SetParent(statChangeLayout.transform);

            statChanges[i] = panel.GetComponent<CharStatChange>();
        }       
    }

    public void UpdateDesc(Equipment merch)
    {
        changeStatText.text = merch.GetItemType();
        this.buyValue = merch.BuyValue;

        CharStats[] equippables = GameManager.Instance.GetEquippableChars(merch);

        for (int i = 0; i < equippables.Length; i++)
        {
            CharStats currStats = equippables[i];
            statChanges[i].gameObject.SetActive(true);

            if (currStats.EquippedWeapon.ItemName.Equals(merch.ItemName))
            {                
                statChanges[i].DisplayUnchangeStat(currStats.CharacterName, merch.GetCorresStat(currStats));
            }
            else
            {                
                statChanges[i].DisplayChangeStat(currStats.CharacterName, merch.GetCorresStat(currStats), merch.GetPostChangeStat(currStats));
            }
        }

        for (int i = equippables.Length; i < statChanges.Length; i++)
        {
            statChanges[i].gameObject.SetActive(false);
        }

        currentGoldText.text = ItemManager.Instance.CurrentGold.ToString();
    }

    public void UpdateDesc(ConsumableItem merch)
    {
        changeStatText.text = merch.GetItemType();
    }

    private void Reset()
    {
        changeStatText.text = "";
        foreach (CharStatChange statChange in statChanges) statChange.gameObject.SetActive(false);
    }

    void ILiveAmountObserver.OnValueChanged(int value)
    {
        totalCostText.text = value * buyValue + " G";
    }

}
