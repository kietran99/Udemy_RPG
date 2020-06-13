using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "RPG Generator/Items/Equipments/Armor")]
public class Armor : Protector
{
    private Armor nullArmor;

    private void Awake()
    {
        nullArmor = Resources.Load<Armor>(NullEquipmentsRef.noArmor);
    }

    public override int GetPostChangeStat(CharStats stats)
    {
        return stats.Defence + statChange - stats.EquippedArmor.StatChange;
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Defence = GetPostChangeStat(stats);

        if (stats.EquippedArmor.Equals(nullArmor) || !stats.EquippedArmor.Equals(this)) stats.EquippedArmor = this; 
        else stats.EquippedArmor = nullArmor;       
    }
}
