using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserChooser : MonoBehaviour
{
    [SerializeField]
    private GameObject firstButton = null, buttonsContainer = null; 
    
    [SerializeField]
    private Text remainingText = null;

    private ItemsDisplay display;

    private int numOfRemaining;

    private GameObject itemInteractor;

    private UserButton[] userButtons;

    private EntityStats.Attributes changingAttr;

    private bool isHPMP;

    public void Activate(GameObject itemInteractor, ItemsDisplay display)
    {               
        this.itemInteractor = itemInteractor;
        this.display = display;
        SetBtnsInteraction();
        this.itemInteractor.SetActive(false);
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
            gameObject.SetActive(false);
        }
    }

    private void Cancel()
    {
        for (int i = 0; i < userButtons.Length; i++)
        {
            if (i != 0) Destroy(userButtons[i].gameObject);
            else firstButton.SetActive(false); 
        }

        SetBtnsInteraction(true);
        display.ToDefaultState();
        itemInteractor.SetActive(true);      
    }

    private void OnEnable()
    {
        InitButtonsGUI();
        DisplayParty();
    }

    private void OnDisable()
    {
        Cancel();
    }

    private void SetBtnsInteraction(bool flag = false)
    {
        // Disable button click for every button in the display including this gameObject's buttons
        foreach (Button button in display.gameObject.GetComponentsInChildren<Button>())
        {
            button.interactable = flag;
        }

        // Enable button click for this gameObject's buttons
        foreach (Button button in gameObject.GetComponentsInChildren<Button>())
        {
            button.interactable = !flag;
        }
    }

    private void InitButtonsGUI()
    {
        int numOfActives = GameManager.Instance.GetNumActives();
        userButtons = new UserButton[numOfActives];

        // Init first button
        firstButton.SetActive(true);
        userButtons[0] = firstButton.GetComponent<UserButton>();
        userButtons[0].GetComponent<Button>().onClick.AddListener(() => OnUserSelected(0));

        // Init the rest
        for (int i = 1; i < numOfActives; i++)
        {
            GameObject temp = Instantiate(firstButton);
            temp.transform.SetParent(buttonsContainer.transform);
            userButtons[i] = temp.GetComponent<UserButton>();
            int pos = i;
            userButtons[i].GetComponent<Button>().onClick.AddListener(() => OnUserSelected(pos));
        }
    }

    struct HPMP
    {
        public int current, max;
        public HPMP(int current, int max)
        {
            this.current = current;
            this.max = max;
        }
    }

    private void DisplayParty()
    {
        CharStats[] party = GameManager.Instance.GetActiveChars();

        Item selectedItem = ItemManager.Instance.GetItemAt(display.SelectedPos, display.CurrentPossessor);

        // Set number of items remaining
        numOfRemaining = ItemManager.Instance.GetNumOfItemsAt(display.SelectedPos, display.CurrentPossessor);
        UpdateRemaining();

        // Set character's stat to be changed
        changingAttr = selectedItem.Effects[0].Attribute;

        if (changingAttr == EntityStats.Attributes.HP       ||
            changingAttr == EntityStats.Attributes.MAX_HP   ||
            changingAttr == EntityStats.Attributes.MP       ||
            changingAttr == EntityStats.Attributes.MAX_MP)
        {
            isHPMP = true;

            for (int i = 0; i < party.Length; i++)
            {
                HPMP temp = GetHPMP(changingAttr, party[i]);
                userButtons[i].InitDisplay(party[i].CharacterName, temp.current, temp.max, changingAttr);
            }
        }       
        else
        {
            isHPMP = false;

            for (int i = 0; i < party.Length; i++)
            {
                userButtons[i].InitDisplay(party[i].CharacterName, GetStat(changingAttr, party[i]), changingAttr);
            }
        }
    }

    private int GetStat(EntityStats.Attributes attr, CharStats stats)
    {
        switch (attr)
        {            
            case EntityStats.Attributes.STR:    return stats.Strength;
            case EntityStats.Attributes.DEF:    return stats.Defence;
            case EntityStats.Attributes.INT:    return stats.Intellect;
            case EntityStats.Attributes.VIT:    return stats.Vitality;
            case EntityStats.Attributes.AGI:    return stats.Agility;
            case EntityStats.Attributes.LCK:    return stats.Luck;
            case EntityStats.Attributes.EXP:    return stats.CurrentEXP;
            default:                            return -1;
        }
    }

    private HPMP GetHPMP(EntityStats.Attributes attr, CharStats stats)
    {
        switch (attr)
        {
            case EntityStats.Attributes.HP: 
            case EntityStats.Attributes.MAX_HP: return new HPMP(stats.CurrentHP, stats.MaxHP);
            case EntityStats.Attributes.MP: 
            case EntityStats.Attributes.MAX_MP: return new HPMP(stats.CurrentMP, stats.MaxMP);
            default:                            return new HPMP(0, 0);
        }
    }

    public void OnUserSelected(int pos)
    {
        CharStats selectedUser = GameManager.Instance.GetCharacterAt(pos);
        ItemManager.Instance.UseItem(display.CurrentPossessor, display.SelectedPos, selectedUser);
        display.DisplayAll();

        // Decrease number of remaining items
        numOfRemaining--;

        // If there aren't any remaining items
        if (numOfRemaining == 0)
        {
            Cancel();
            gameObject.SetActive(false);
            return;
        }
        
        // Else
        UpdateRemaining();

        if (isHPMP)
        {
            HPMP temp = GetHPMP(changingAttr, selectedUser);
            userButtons[pos].DisplayStat(temp.current, temp.max);
        }
        else
        {
            userButtons[pos].DisplayStat(GetStat(changingAttr, selectedUser));
        }
    }

    private void UpdateRemaining()
    { 
        remainingText.text = numOfRemaining.ToString();
    }
}
