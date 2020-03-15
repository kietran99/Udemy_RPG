using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Name", menuName = "RPG Generator/Character Name", order = 51)]
public class CharName : ScriptableObject
{
    public string CharacterName { get { return characterName; } }

    [SerializeField]
    private string characterName = "";    
}
