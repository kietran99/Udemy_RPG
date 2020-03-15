using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityStats : MonoBehaviour
{
    public enum Attributes
    {
        HP,
        MAX_HP,
        MP,
        MAX_MP,
        EXP,
        STR,
        DEF,
        INT,
        VIT,
        AGI,
        LCK
    }

    #region
    public string EntityName { get { return entityName; } }
    public string Bio { get { return bio; } set { bio = value; } }
    public Sprite EntityImage { get { return entityImage; } }
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int CurrentMP { get { return currentMP; } set { currentMP = value; } }
    public int MaxMP { get { return maxMP; } set { maxMP = value; } }
    public int Strength { get { return strength; } set { strength = value; } }
    public int Defence { get { return defence; } set { defence = value; } }
    public int Intellect { get { return intellect; } set { intellect = value; } }
    public int Vitality { get { return vitality; } set { vitality = value; } }
    public int Agility { get { return agility; } set { agility = value; } }
    public int Luck { get { return luck; } set { luck = value; } }
    #endregion

    [Header("General")]

    [SerializeField]
    private string entityName = "";

    [SerializeField]
    private string bio = "";

    [SerializeField]
    private Sprite entityImage = null;

    [Header("HP & MP")]

    [SerializeField]
    private int currentHP = 0;

    [SerializeField]
    private int maxHP = 0;

    [SerializeField]
    private int currentMP = 0;

    [SerializeField]
    private int maxMP = 0;

    [Header("Attributes")]

    [SerializeField]
    private int strength = 0;

    [SerializeField]
    private int defence = 0, intellect = 0, vitality = 0, agility = 0, luck = 0;
}
