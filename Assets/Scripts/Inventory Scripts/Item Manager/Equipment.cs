using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    protected const string primaryAction = "EQUIP";

    public override string GetPrimaryAction()
    {
        return primaryAction;
    }
}
