using UnityEngine;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
    [SerializeField]
    private Text userNameText = null, userStatText = null;

    private string changingAttr;
    
    private string TranslateAttr(EntityStats.Attributes attr)
    {
        switch(attr)
        {
            case EntityStats.Attributes.STR:    return "STR";
            case EntityStats.Attributes.DEF:    return "DEF";
            case EntityStats.Attributes.INT:    return "INT";
            case EntityStats.Attributes.VIT:    return "VIT";
            case EntityStats.Attributes.AGI:    return "AGI";
            case EntityStats.Attributes.LCK:    return "LCK";
            case EntityStats.Attributes.HP:
            case EntityStats.Attributes.MAX_HP: return "HP";
            case EntityStats.Attributes.MP:
            case EntityStats.Attributes.MAX_MP: return "MP";
            case EntityStats.Attributes.EXP:    return "EXP";
            default:                            return "";
        }
    }

    public void InitDisplay(string userName, int userStat, EntityStats.Attributes attr)
    {
        userNameText.text = userName;
        changingAttr = TranslateAttr(attr);
        DisplayStat(userStat);
    }

    public void InitDisplay(string userName, int userStat, int maxStat, EntityStats.Attributes attr)
    {
        userNameText.text = userName;
        changingAttr = TranslateAttr(attr);
        DisplayStat(userStat, maxStat);
    }

    public void DisplayStat(int newUserStat)
    {
        userStatText.text = changingAttr + ": " + newUserStat;
    }

    public void DisplayStat(int newUserStat, int newMaxStat)
    {
        userStatText.text = changingAttr + ": " + newUserStat + "/" + newMaxStat;
    }
}
