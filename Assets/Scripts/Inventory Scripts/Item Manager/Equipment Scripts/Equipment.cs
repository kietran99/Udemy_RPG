using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Equipment : Item
{
    #region
    public CharName[] EquippableChars { get { return equippableChars; } }
    public int StatChange { get { return statChange; } }
    #endregion

    [SerializeField]
    protected int statChange;

    [SerializeField]
    protected CharName[] equippableChars;

    private string currentAction;

    private void Awake()
    {
        currentAction = EQUIP_ACTION;
    }

    public override string GetPrimaryAction()
    {
        return currentAction;
    }

    public override void SetPrimaryAction(bool isEquipped)
    {
        if (isEquipped) currentAction = UNEQUIP_ACTION;
        else currentAction = EQUIP_ACTION;
    }

    public override void InvokePrimaryAction(CharStats stats)
    {
        ToggleEquipAbility(stats);
    }

    public abstract void ToggleEquipAbility(CharStats stats);   

    public bool CanEquip(string charName)
    { 
        if (equippableChars.Where(x => x.CharacterName.Equals(charName)).ToArray().Length > 0) return true;

        return false;
    }

    public abstract int GetCorreStat(CharStats stats);
    public abstract int GetPostChangeStat(CharStats stats);
}
