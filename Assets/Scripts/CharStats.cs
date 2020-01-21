using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string characterName;

    // Level related stats
    public int playerLevel = 1;
    public int currentEXP;

    public int[] EXPToNextLevel;

    private const int maxLevel = 100;
    public int baseEXP = 1000;

    // HP and MP
    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;
    public int[] mpLvlBonus;

    // Character's attributes
    public int strength, defence, intellect, vitality, agility;

    public string equippedWeapon, equippedArmor, equippedHelmet, equippedSecondary, equippedShoes, equippedAccessory;

    // Avatar
    public Sprite charImage;

    public float levelMultiplier = 1.05f;

    // Start is called before the first frame update
    void Start()
    {
        InitEXPs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            AddEXP(500);
        }
    }

    private void InitEXPs()
    {
        EXPToNextLevel = new int[maxLevel];
        EXPToNextLevel[0] = baseEXP;

        for (int i = 1; i < maxLevel; i++)
        {
            EXPToNextLevel[i] = Mathf.FloorToInt(EXPToNextLevel[i - 1] * levelMultiplier);
        }
    }

    private void AddEXP(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel && currentEXP >= EXPToNextLevel[playerLevel - 1])
        {
            currentEXP -= EXPToNextLevel[playerLevel - 1];
            playerLevel++;

            IncreaseStats();
        }
    }

    private void IncreaseStats()
    {
        if (playerLevel % 2 == 0)
        {
            strength++;
        }
        else
        {
            defence++;
        }

        maxHP = Mathf.FloorToInt(maxHP * levelMultiplier);

        maxMP += mpLvlBonus[playerLevel - 1];
    }
}
