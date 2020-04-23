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
        hpText.text = "" + playerStats.CurrentHP + "/" + playerStats.MaxHP;
        mpText.text = "" + playerStats.CurrentMP + "/" + playerStats.MaxMP;
        hpSlider.maxValue = playerStats.MaxHP;
        hpSlider.value = playerStats.CurrentHP;
        mpSlider.maxValue = playerStats.MaxMP;
        mpSlider.value = playerStats.CurrentMP;
        charImage.sprite = playerStats.CharImage;

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
