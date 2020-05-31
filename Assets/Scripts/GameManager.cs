using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region
    public static GameManager Instance { get { return instance; } set { instance = value; } }
    public CharStats[] PlayerStats { get { return playerStats; } }  
    #endregion

    private static GameManager instance;

    public const int MAX_PARTY_MEMBERS = 5;

    [SerializeField]
    private CharStats[] playerStats = null;
    
    [HideInInspector]
    public bool fadingBetweenAreas, dialogActive, gameMenuActive, shopMenuActive;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingBetweenAreas || dialogActive || gameMenuActive || shopMenuActive) PlayerController.Instance.canMove = false;
        else PlayerController.Instance.canMove = true;
    }

    public int GetNumActives()
    {
        return playerStats.Where(x => x.gameObject.activeInHierarchy).ToArray().Length;
    }

    public CharStats[] GetActiveChars()
    {
        return playerStats.Where(x => x.gameObject.activeInHierarchy).ToArray();
    }

    public CharStats GetCharacterAt(int pos)
    {
        return playerStats[pos];
    }

    public CharStats[] GetEquippableChars(Equipment equipment)
    {
        List <CharStats> equippables = new List<CharStats>();

        CharStats[] activeChars = GetActiveChars();

        foreach (CharName charName in equipment.EquippableChars)
        {
            equippables.Add(activeChars.Where(x => x.CharacterName.Equals(charName.CharacterName)).ToArray()[0]);
        }

        return equippables.ToArray();
    }
}
