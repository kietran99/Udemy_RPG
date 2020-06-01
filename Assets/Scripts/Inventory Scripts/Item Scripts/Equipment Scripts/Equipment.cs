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

    public abstract int GetCorresStat(CharStats stats);
    public abstract int GetPostChangeStat(CharStats stats);

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
        return Functional.HigherOrderFunc.Filter(x => x.CharacterName.Equals(charName), equippableChars).Length > 0;
    }

    public CharStats[] GetEquippableChars()
    {        
        CharStats[] activeChars = GameManager.Instance.GetActiveChars();     
        return Functional.HigherOrderFunc.Filter(x => CanEquip(x.CharacterName), activeChars);
    }
}
