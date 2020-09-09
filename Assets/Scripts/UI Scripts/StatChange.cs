using UnityEngine;
using UnityEngine.UI;

public class StatChange : MonoBehaviour
{
    [SerializeField]
    private Text currentText = null, afterText = null;

    public void Show(string current, string after, Color color)
    {
        currentText.color = color;
        afterText.color = color;
        currentText.text = current;
        afterText.text = after;
    }
}
