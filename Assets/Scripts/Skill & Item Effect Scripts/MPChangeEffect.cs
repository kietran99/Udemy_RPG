using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MP Change Effect", menuName = "RPG Generator/Effects/New MP Change Effect")]
public class MPChangeEffect : Effect
{
    private void Awake()
    {
        attribute = EntityStats.Attributes.MP;
    }

    public override void Init(int changeAmount)
    {
        this.changeAmount = changeAmount;        
    }

    public override void Invoke(CharStats stats)
    {
        stats.CurrentMP += changeAmount;

        if (stats.CurrentMP > stats.MaxMP) stats.CurrentMP = stats.MaxMP;
    }

}
