using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region
    public static DialogManager Instance { get { return instance; } set { instance = value; } }
    public GameObject DialogBox { get { return dialogBox; } }
    #endregion

    private static DialogManager instance;

    [SerializeField]
    private GameObject dialogBox = null, nameBox = null;

    [SerializeField]
    private Text dialogText = null, nameText = null;

    private string[] dialogLines;

    private int currentLine;

    // Make each line be typed more fluidly
    [SerializeField]
    private float charWaitTime = 0.02f;
    private bool isTyping = false;

    // Seconds to next dialog display since closing current dialog
    [SerializeField]
    private const float dialogDelay = 1f;
    private float secsToNextDialog;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (secsToNextDialog >= 0f)
        {
            secsToNextDialog -= Time.deltaTime;
            return;
        }

        if (dialogBox.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopCoroutine("TypeSentence");
                isTyping = false;
                dialogText.text = dialogLines[currentLine];
                return;
            }

            currentLine++;

            if (currentLine >= dialogLines.Length)
            {
                dialogBox.SetActive(false);
                GameManager.Instance.dialogActive = false;
                secsToNextDialog = dialogDelay;
            }
            else
            {
                StartCoroutine("TypeSentence");
            }
        }
    }

    public bool CanLoadNextSentence()
    {
        return secsToNextDialog <= 0f;
    }

    private IEnumerator TypeSentence()
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in dialogLines[currentLine].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(charWaitTime);
        }

        isTyping = false;
    }

    public void InitDialog(string speaker, string[] dialogLines, bool isPerson)
    {
        GameManager.Instance.dialogActive = true;

        this.dialogLines = dialogLines;

        currentLine = 0;
        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(true);

        if (isPerson)
        {
            nameBox.SetActive(true);
            nameText.text = speaker;
        }
        else
        {
            nameBox.SetActive(false);
        }
    }
}
