using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : UIDisplay
{
    public CircularLinkedList<CharStats> linkedPlayerStats;
    private CharStats[] playerStats;

    public Image charImage;
    public Text nameText, lvText, classText, strengthText, defenceText, intellectText, vitalityText, agilityText, luckText,
                maxHPText, maxMPText, currentEXPText, toNextLvText, descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameManager.Instance.PlayerStats;
        linkedPlayerStats = new CircularLinkedList<CharStats>();
        linkedPlayerStats.Append(playerStats);
        DisplayAll();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if (linkedPlayerStats == null) return;
        linkedPlayerStats.RevertToDefault();
        DisplayAll();
    }

    // Iterate through the next player that is active
    public void NextChar()
    {
        do
        {
            linkedPlayerStats.NextPos();
        }
        while (!linkedPlayerStats.current.value.gameObject.activeInHierarchy); 

        DisplayAll();
    }

    public void PrevChar()
    {
        do
        {
            linkedPlayerStats.PrevPos();
        }
        while (!linkedPlayerStats.current.value.gameObject.activeInHierarchy);

        DisplayAll();
    }

    public override void DisplayAll()
    {
        CharStats currentCharacter = linkedPlayerStats.current.value;

        charImage.sprite = currentCharacter.CharImage;
        nameText.text = currentCharacter.CharacterName;
        lvText.text = "Lv: " + currentCharacter.PlayerLevel;
        //classText.text = currentCharacter.CharClass;
        strengthText.text = currentCharacter.Strength.ToString();
        defenceText.text = currentCharacter.Defence.ToString();
        intellectText.text = currentCharacter.Intellect.ToString();
        vitalityText.text = currentCharacter.Vitality.ToString();
        agilityText.text = currentCharacter.Agility.ToString();
        luckText.text = currentCharacter.Luck.ToString();
        maxHPText.text = currentCharacter.MaxHP.ToString();
        maxMPText.text = currentCharacter.MaxMP.ToString();
        currentEXPText.text = currentCharacter.CurrentEXP.ToString();
        toNextLvText.text = (currentCharacter.EXPToNextLevel[currentCharacter.PlayerLevel - 1] - currentCharacter.CurrentEXP).ToString();
        descriptionText.text = currentCharacter.Bio;
    }
}
