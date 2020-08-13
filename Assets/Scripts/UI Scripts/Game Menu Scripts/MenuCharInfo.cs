using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuCharInfo : MonoBehaviour
{
    [SerializeField]
    private Text nameText = null, lvText = null, hpText = null, mpText = null;

    [SerializeField]
    private Slider hpSlider = null, mpSlider = null;

    [SerializeField]
    private Image charImage = null;

    public void Activate(CharStats playerStats)
    {
        nameText.text = playerStats.CharacterName;
        lvText.text = "Lv: " + playerStats.PlayerLevel.ToString();
        hpText.text = AmountStringFormat(playerStats.CurrentHP, playerStats.MaxHP);
        mpText.text = AmountStringFormat(playerStats.CurrentMP, playerStats.MaxMP);
        hpSlider.maxValue = playerStats.MaxHP;
        hpSlider.value = playerStats.CurrentHP;
        mpSlider.maxValue = playerStats.MaxMP;
        mpSlider.value = playerStats.CurrentMP;
        charImage.sprite = playerStats.CharImage;

        gameObject.SetActive(true);
    }

    private string AmountStringFormat(int current, int max)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append((current < 10) ? "0" : "");
        sb.Append(current);
        sb.Append("/");
        sb.Append((max < 10) ? "0" : "");
        sb.Append(max);
        return sb.ToString();
    }
}
