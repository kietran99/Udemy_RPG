using UnityEngine;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
    [SerializeField]
    private Text userNameText = null, userStatText = null;

    private string changingAttribName;
        
    public void InitStat(EntityStats.Attributes attr, string userName, int userStat, int maxStat = -1)
    {
        userNameText.text = userName;
        changingAttribName = new StatFetcher(attr).ExtractName();
        UpdateStat(userStat, maxStat);
    }
    
    public void UpdateStat(int newUserStat, int newMaxStat)
    {
        userStatText.text = changingAttribName + ": " + newUserStat + (newMaxStat == Constants.INVALID ? "" : "/" + newMaxStat);
    }
}
