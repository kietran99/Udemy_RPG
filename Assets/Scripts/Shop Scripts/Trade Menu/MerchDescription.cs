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

    private int tradeValue;

    [SerializeField]
    private Text totalCostText = null;

    [SerializeField]
    private Text currentGoldText = null;

    [SerializeField]
    private Text itemDescText = null;

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

    public void UpdateDesc(Equipment merch, int tradeValue)
    {
        Reset();

        changeStatText.text = merch.GetItemType();
        currentGoldText.text = ItemManager.Instance.CurrentGold + " G";
        this.tradeValue = tradeValue;
        totalCostText.text = this.tradeValue.ToString();
        itemDescText.text = merch.Description;

        CharStats[] equippables = GameManager.Instance.GetEquippableChars(merch);

        // Display equippable characters' stats changes
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
    }

    //public void UpdateDesc(ConsumableItem merch)
    //{
    //    changeStatText.text = merch.GetItemType();
    //    currentGoldText.text = ItemManager.Instance.CurrentGold.ToString();
    //    itemDescText.text = merch.Description;
    //}

    public void UpdateDesc(Item merch, int tradeValue)
    {
        Reset();
        changeStatText.text = merch.GetItemType();
        currentGoldText.text = ItemManager.Instance.CurrentGold.ToString();
        itemDescText.text = merch.Description;
        this.tradeValue = tradeValue;
        totalCostText.text = tradeValue.ToString();
    }

    private void Reset()
    {
        changeStatText.text = "";
        itemDescText.text = "";
        foreach (CharStatChange statChange in statChanges) statChange.gameObject.SetActive(false);
    }

    void ILiveAmountObserver.OnValueChanged(int value)
    {
        totalCostText.text = value * tradeValue + " G";
    }

}
