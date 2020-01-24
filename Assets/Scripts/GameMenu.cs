using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

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
        GameManager.instance.openingGameMenu = true;
    }

    public void CloseMenu()
    {
        foreach (GameObject window in windows) window.SetActive(false);
        theMenu.SetActive(false);
        GameManager.instance.openingGameMenu = false;
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolders[i].SetActive(true);

                nameTexts[i].text = playerStats[i].characterName;
                lvTexts[i].text = "Lv: " + playerStats[i].playerLevel.ToString();
                hpTexts[i].text = "" + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpTexts[i].text = "" + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                hpSliders[i].maxValue = playerStats[i].maxHP;
                hpSliders[i].value = playerStats[i].currentHP;
                mpSliders[i].maxValue = playerStats[i].maxMP;
                mpSliders[i].value = playerStats[i].currentMP;
                charImages[i].sprite = playerStats[i].charImage;
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
