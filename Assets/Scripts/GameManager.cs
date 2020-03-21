using System.Collections;
using System.Collections.Generic;
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

    public int GetNumberOfActives()
    {
        int num = 0;

        foreach (CharStats stats in playerStats)
        {
            if (stats.gameObject.activeInHierarchy) num++;
        }

        return num;
    }

    public CharStats[] GetActiveMembers()
    {
        List<CharStats> temp = new List<CharStats>();

        foreach (CharStats stats in playerStats)
        {
            if (stats.gameObject.activeInHierarchy) temp.Add(stats);
        }

        return temp.ToArray();
    }

    public CharStats GetCharacterAt(int pos)
    {
        return playerStats[pos];
    }
}
