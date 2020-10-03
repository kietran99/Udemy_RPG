using UnityEngine;
using UnityEngine.UI;

public class MerchDescription : MonoBehaviour
{
    [SerializeField]
    private Text changeStatText = null;

    [SerializeField]
    private GameObject statChangePrototype = null;

    [SerializeField]
    private GameObject statChangeLayout = null;

    private CharStatChange[] statChanges;

    private int tradeValue;

    [SerializeField]
    private Text totalCostText = null;

    [SerializeField]
    private Text currentGoldText = null;

    [SerializeField]
    private Text itemDescText = null;

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

    public void UpdateDesc(Equipment merch, int tradeValue)
    {
        CharStats[] equippables = merch.GetEquippableChars();
        UpdateTextsAndTradeValue(merch, tradeValue);       
        DisplayStatsChange(equippables, merch);     
        HideUnequippableChars(equippables.Length);
    }

    public void UpdateDesc(Item merch, int tradeValue)
    {
        UpdateTextsAndTradeValue(merch, tradeValue);
    }

    private void UpdateTextsAndTradeValue(Item merch, int tradeValue)
    {
        Reset();
        this.tradeValue = tradeValue;
        changeStatText.text = merch.GetItemType();
        currentGoldText.text = GameManager.Instance.CurrentGold + " G";
        itemDescText.text = merch.Description;       
        totalCostText.text = tradeValue.ToString();
    }

    private void DisplayStatsChange(CharStats[] equippables, Equipment merch)
    {
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
                statChanges[i].DisplayChangeStat(currStats.CharacterName, merch.GetCorresStat(currStats), merch.GetLaterStat(currStats));
            }
        }
    }

    private void HideUnequippableChars(int nEquippables)
    {
        for (int i = nEquippables; i < statChanges.Length; i++)
        {
            statChanges[i].gameObject.SetActive(false);
        }
    }

    private void Reset()
    {
        changeStatText.text = "";
        itemDescText.text = "";
        foreach (CharStatChange statChange in statChanges) statChange.gameObject.SetActive(false);
    }

    public void UpdateShownValue(int value)
    {
        totalCostText.text = value * tradeValue + " G";
    }

}
