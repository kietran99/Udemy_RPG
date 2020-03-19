using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string speaker;

    public string[] dialogLines;

    public bool isPerson = true;

    private bool canActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canActivate && 
            !DialogManager.Instance.DialogBox.activeInHierarchy &&
            DialogManager.Instance.SecsToNextDialog <= 0f) // if DialogManager is not being delayed
            DialogManager.Instance.InitDialog(speaker, dialogLines, isPerson); 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canActivate = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canActivate = false;
    }
}
