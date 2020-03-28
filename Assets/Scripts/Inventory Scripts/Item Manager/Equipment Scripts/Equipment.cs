using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public abstract class Equipment : Item
{
    public const string pathToBareEquipment = "Assets/Scriptable Objects/Items/Equipments/Bare Equipment.asset";      

    [SerializeField]
    protected CharName[] equipableCharacters;

    protected Equipment bareEquipment;

    private string currentAction;
    
    private void Awake()
    {
        currentAction = EQUIP_ACTION;
        bareEquipment = (Equipment) AssetDatabase.LoadAssetAtPath(pathToBareEquipment, typeof(Equipment));
    }

    public abstract string GetStatBoostName();

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
        if (equipableCharacters.Where(x => x.CharacterName.Equals(charName)).ToArray().Length > 0) return true;

        return false;
    }

}
