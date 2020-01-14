using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogBox, nameBox;
    public Text dialogText, nameText;

    public string[] dialogLines;

    public int currentLine;

    // make each line is typed more fluid
    private const float charWaitTime = 0.02f;
    private bool isTyping = false;

    // seconds to next dialog display since closing current dialog
    private const float dialogDelay = 1f;
    public float secsToNextDialog;

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
        // disable any dialog activation for dialogDelay seconds
        if (secsToNextDialog >= 0f)
        {
            secsToNextDialog -= Time.deltaTime;
            return;
        }

        if (dialogBox.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            // if a dialog sentence is being typed, stop the current typing then show the whole sentence
            if (isTyping)
            {
                StopCoroutine("TypeSentence");
                isTyping = false;
                dialogText.text = dialogLines[currentLine];
                return;
            }

            // else move to next line of dialog
            currentLine++;

            if (currentLine >= dialogLines.Length)
            {
                dialogBox.SetActive(false);
                secsToNextDialog = dialogDelay;
            }
            else
            {
                StartCoroutine("TypeSentence");
            }
        }
    }

    public IEnumerator TypeSentence()
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
