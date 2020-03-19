using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharStats : MonoBehaviour
{
    #region
    public string CharacterName { get { return characterName; } }
    public string Bio { get { return bio; } set { bio = value; } }
    public Sprite CharImage { get { return charImage; } }
    public CharacterClass CharClass { get { return charClass; } set { charClass = value; } }
    public int PlayerLevel { get { return playerLevel; } set { playerLevel = value; } }
    public int CurrentEXP { get { return currentEXP; } set { currentEXP = value; } }
    public int[] EXPToNextLevel { get { return expToNextLevel; } set { expToNextLevel = value; } }  
    public int BaseEXP { get { return baseEXP; } set { baseEXP = value; } }
    public float LevelMultiplier { get { return levelMultiplier; } set { levelMultiplier = value; } }
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int CurrentMP { get { return currentMP; } set { currentMP = value; } }
    public int MaxMP { get { return maxMP; } set { maxMP = value; } }
    public int[] MPLvlBonus { get { return mpLvlBonus; } set { mpLvlBonus = value; } }
    public int Strength { get { return strength; } set { strength = value; } }
    public int Defence { get { return defence; } set { defence = value; } }
    public int Intellect { get { return intellect; } set { intellect = value; } }
    public int Vitality { get { return vitality; } set { vitality = value; } }
    public int Agility { get { return agility; } set { agility = value; } }
    public int Luck { get { return luck; } set { luck = value; } }
    public Equipment EquippedWeapon { get { return equippedWeapon; } set { equippedWeapon = value; } }
    public Equipment EquippedArmor { get { return equippedArmor; } set { equippedArmor = value; } }
    public Equipment EquippedHelmet { get { return equippedHelmet; } set { equippedHelmet = value; } }
    public Equipment EquippedSecondary { get { return equippedSecondary; } set { equippedSecondary = value; } }
    public Equipment EquippedFootwear { get { return equippedFootwear; } set { equippedFootwear = value; } }
    public Equipment EquippedAccessory { get { return equippedAccessory; } set { equippedAccessory = value; } }
    #endregion

    [Header("General")]

    [SerializeField]
    private string characterName = "";

    [SerializeField]
    private string bio = "";

    [SerializeField]
    private Sprite charImage = null;

    [SerializeField]
    private CharacterClass charClass = null;

    [Header("Levelling")]

    [SerializeField]
    private int playerLevel = 1;

    [SerializeField]
    private int currentEXP = 0;

    [SerializeField]
    private int[] expToNextLevel;

    [SerializeField]
    private int baseEXP = 1000;

    private const int maxLevel = 100;
    public float levelMultiplier = 1.05f;

    [Header("HP & MP")]

    [SerializeField]
    private int currentHP = 0;

    [SerializeField]
    private int maxHP = 0;

    [SerializeField]
    private int currentMP = 0;

    [SerializeField]
    private int maxMP = 0;

    [SerializeField]
    private int[] mpLvlBonus = null;

    [Header("Attributes")]

    [SerializeField]
    private int strength = 0;

    [SerializeField]
    private int defence = 0, intellect = 0, vitality = 0, agility = 0, luck = 0;

    [Header("Equipments")]
    private Equipment equippedWeapon, equippedArmor, equippedHelmet, equippedSecondary, equippedFootwear, equippedAccessory;

    // Start is called before the first frame update
    void Awake()
    {
        mpLvlBonus = new int[maxLevel];
        InitEXPs();
        InitEquipments();
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
        expToNextLevel = new int[maxLevel];
        expToNextLevel[0] = baseEXP;

        for (int i = 1; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * LevelMultiplier);
        }
    }

    private void InitEquipments()
    {
        Equipment bareEquipment = (Equipment)AssetDatabase.LoadAssetAtPath(Equipment.pathToBareEquipment, typeof(Equipment));
        equippedWeapon = bareEquipment;
        equippedArmor = bareEquipment;
        equippedHelmet = bareEquipment;
        equippedFootwear = bareEquipment;
        equippedSecondary = bareEquipment;
        equippedAccessory = bareEquipment;
    }

    private void AddEXP(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel && currentEXP >= expToNextLevel[playerLevel - 1])
        {
            currentEXP -= expToNextLevel[playerLevel - 1];
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

        maxHP = Mathf.FloorToInt(maxHP * LevelMultiplier);

        maxMP += mpLvlBonus[playerLevel - 1];
    }
}
