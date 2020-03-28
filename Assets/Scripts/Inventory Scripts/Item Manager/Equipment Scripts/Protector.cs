using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Protector : Equipment
{
    public override string GetStatBoostName()
    {
        return "Defence";
    }
}
