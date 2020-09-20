using UnityEngine;

public abstract class Equipment : Item
{
    #region PROPERTIES
    public CharName[] EquippableChars { get { return equippableChars; } }
    public int StatChange { get { return statChange; } }
    protected Equipment NullEquipment { get; set; }
    public override bool IsEquipment { get => true; }
    #endregion

    [SerializeField]
    protected int statChange;

    [SerializeField]
    protected CharName[] equippableChars;

    public abstract int GetCorresStat(CharStats stats);
    public abstract int GetLaterStat(CharStats stats);
    public abstract AttributesData GetLaterChangeStat(CharStats stats);
   
    public abstract void ToggleEquipAbility(CharStats stats);   

    public bool CanEquip(string charName) => Functional.HOF.Filter(x => x.CharacterName.Equals(charName), equippableChars).Length > 0;

    public CharStats[] GetEquippableChars()
    {        
        CharStats[] activeChars = GameManager.Instance.GetActiveChars();     
        return Functional.HOF.Filter(x => CanEquip(x.CharacterName), activeChars);
    }

    protected Equipment ToggleCharEquipment(Equipment currentEquipment, out bool shouldEquip)
    {
        shouldEquip = currentEquipment.Equals(NullEquipment) || !ReferenceEquals(currentEquipment, this);
        return shouldEquip ? this : NullEquipment;
    }

    protected int UpdateStat(int laterStat, bool shouldEquip) => laterStat - (shouldEquip ? 0 : statChange);
}
