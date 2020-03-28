using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

    [SerializeField]
    private Text currentGoldText = null;

    private CharStats[] playerStats;

    public Text[] nameTexts, lvTexts, hpTexts, mpTexts;
    public Slider[] hpSliders, mpSliders;
    public Image[] charImages;
    public GameObject[] charStatsHolders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else 
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        theMenu.SetActive(true);
        UpdateMainStats();
        currentGoldText.text = ItemManager.Instance.CurrentGold.ToString();
        GameManager.Instance.gameMenuActive = true;
    }

    public void CloseMenu()
    {
        foreach (GameObject window in windows) window.SetActive(false);
        theMenu.SetActive(false);
        GameManager.Instance.gameMenuActive = false;
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.Instance.PlayerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolders[i].SetActive(true);

                nameTexts[i].text = playerStats[i].CharacterName;
                lvTexts[i].text = "Lv: " + playerStats[i].PlayerLevel.ToString();
                hpTexts[i].text = "" + playerStats[i].CurrentHP + "/" + playerStats[i].MaxHP;
                mpTexts[i].text = "" + playerStats[i].CurrentMP + "/" + playerStats[i].MaxMP;
                hpSliders[i].maxValue = playerStats[i].MaxHP;
                hpSliders[i].value = playerStats[i].CurrentHP;
                mpSliders[i].maxValue = playerStats[i].MaxMP;
                mpSliders[i].value = playerStats[i].CurrentMP;
                charImages[i].sprite = playerStats[i].CharImage;
            }
            else
            {
                charStatsHolders[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int idx)
    {
        UpdateMainStats();

        for(int i = 0; i < windows.Length; i++)
        {
            if (i == idx)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }
}
