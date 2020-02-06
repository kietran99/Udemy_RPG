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
                maxHPText, maxMPText, currentEXPText, toNextLvText, descriptionText;

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

    void OnEnable()
    {
        if (linkedPlayerStats != null) Display();
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
        CharStats currentCharacter = linkedPlayerStats.current.value;

        charImage.sprite = currentCharacter.charImage;
        nameText.text = currentCharacter.characterName;
        lvText.text = "Lv: " + currentCharacter.playerLevel;
        classText.text = currentCharacter.charClass;
        strengthText.text = currentCharacter.strength.ToString();
        defenceText.text = currentCharacter.defence.ToString();
        intellectText.text = currentCharacter.intellect.ToString();
        vitalityText.text = currentCharacter.vitality.ToString();
        agilityText.text = currentCharacter.agility.ToString();
        luckText.text = currentCharacter.luck.ToString();
        maxHPText.text = currentCharacter.maxHP.ToString();
        maxMPText.text = currentCharacter.maxMP.ToString();
        currentEXPText.text = currentCharacter.currentEXP.ToString();
        toNextLvText.text = (currentCharacter.EXPToNextLevel[currentCharacter.playerLevel - 1] - currentCharacter.currentEXP).ToString();
        descriptionText.text = currentCharacter.description;
    }
}
