using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    public CircularLinkedList<CharStats> linkedPlayerStats;
    private CharStats[] playerStats;

    public Image charImage;
    public Text nameText, lvText, classText, strengthText, defenceText, intellectText, vitalityText, agilityText, luckText,
                maxHPText, maxMPText, currentEXPText, toNextLvText;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameManager.instance.playerStats;
        linkedPlayerStats = new CircularLinkedList<CharStats>();
        linkedPlayerStats.Append(playerStats);
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Iterate through the next player that is active
    public void NextChar()
    {
        do
        {
            linkedPlayerStats.NextPos();
        }
        while (!linkedPlayerStats.current.value.gameObject.activeInHierarchy); 

        Display();
    }

    public void PrevChar()
    {
        do
        {
            linkedPlayerStats.PrevPos();
        }
        while (!linkedPlayerStats.current.value.gameObject.activeInHierarchy);

        Display();
    }

    private void Display()
    {
        CharStats current = linkedPlayerStats.current.value;

        charImage.sprite = current.charImage;
        nameText.text = current.characterName;
        lvText.text = "Lv: " + current.playerLevel;
        classText.text = current.charClass;
        strengthText.text = current.strength.ToString();
        defenceText.text = current.defence.ToString();
        intellectText.text = current.intellect.ToString();
        vitalityText.text = current.vitality.ToString();
        agilityText.text = current.agility.ToString();
        luckText.text = current.luck.ToString();
        maxHPText.text = current.maxHP.ToString();
        maxMPText.text = current.maxMP.ToString();
        currentEXPText.text = current.currentEXP.ToString();
        toNextLvText.text = (current.EXPToNextLevel[current.playerLevel - 1] - current.currentEXP).ToString();
    }
}
