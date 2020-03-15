using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterClass : ScriptableObject
{
    public string ClassName { get { return className; } }

    private string className = null;
}
