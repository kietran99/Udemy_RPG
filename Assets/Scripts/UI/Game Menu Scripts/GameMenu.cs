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

    void Update()
    {
        if (GameManager.Instance.DialogActive) return;

        if (!Input.GetKeyDown(KeyboardControl.General.Inventory)) return;

        if (theMenu.activeInHierarchy)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        theMenu.SetActive(true);
        UpdateMainStats();
        currentGoldText.text = GameManager.Instance.CurrentGold.ToString();
        GameManager.Instance.GameMenuActive = true;
    }

    public void CloseMenu()
    {
        foreach (GameObject window in windows) window.SetActive(false);
        theMenu.SetActive(false);
        GameManager.Instance.GameMenuActive = false;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
