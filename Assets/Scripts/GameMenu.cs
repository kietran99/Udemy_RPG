using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject theMenu = null;

    [SerializeField]
    private GameObject[] windows = null;

    [SerializeField]
    private Text currentGoldText = null;

    [SerializeField]
    private MenuCharInfo[] charInfos = null;

    private CharStats[] playerStats;

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
                charInfos[i].Activate(playerStats[i]);              
            }
            else
            {
                charInfos[i].gameObject.SetActive(false);
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
