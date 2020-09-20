using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect: ScriptableObject
{
    public EntityStats.Attributes Attribute { get { return attribute; } }

    [SerializeField]
    protected int changeAmount = 0;

    protected EntityStats.Attributes attribute;

    public abstract void Init(int changeAmount);
    public abstract void Invoke(CharStats stats);
}
