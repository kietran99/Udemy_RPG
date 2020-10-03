using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager Instance { get { return instance; } set { instance = value; } }
    private static GameManager instance;
    void Awake()
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
    #endregion

    public const int MAX_PARTY_MEMBERS = 5;

    #region PROPERTIES
    public CharStats[] PlayerStats { get { return playerStats; } }
    public int CurrentGold { get { return currentGold; } set { currentGold = value; } }
    public bool FadingBetweenAreas { get; set; }

    public bool DialogActive { get; set; }

    public bool GameMenuActive { get; set; }

    public bool ShopMenuActive { get; set; }
    #endregion

    [SerializeField]
    private int currentGold = 60;

    [SerializeField]
    private CharStats[] playerStats = null;

    void Update()
    {
        PlayerController.Instance.canMove = 
            !FadingBetweenAreas && !DialogActive && !GameMenuActive && !ShopMenuActive;
    }

    public int GetNumActives() => playerStats.Filter(_ => _.gameObject.activeInHierarchy).Length;

    public CharStats[] GetActiveChars() => playerStats.Filter(_ => _.gameObject.activeInHierarchy);

    public CharStats GetCharacterAt(int pos) => playerStats[pos];

    public CharStats GetCharacter(string charName) => playerStats.LookUp(_ => _.CharacterName.Equals(charName)).item;

    public void IncreaseGold(int amount) => currentGold += amount;

    public void DecreaseGold(int amount) => currentGold -= amount;
}
