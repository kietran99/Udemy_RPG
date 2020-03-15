using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HP Change Effect", menuName = "RPG Generator/Effects/New HP Change Effect")]
public class HPChangeEffect : Effect
{
    private void Awake()
    {
        attribute = EntityStats.Attributes.HP;
    }

    public override void Init(int changeAmount)
    {
        this.changeAmount = changeAmount;
    }

    public override void Invoke(CharStats stats)
    {        
        stats.CurrentHP += changeAmount;

        if (stats.CurrentHP > stats.MaxHP) stats.CurrentHP = stats.MaxHP;
    }
}
