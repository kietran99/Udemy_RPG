using Cycler;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCycler : MonoBehaviour, ICycler<CharStats>
{
    public CharStats Current { get { return characters.current.value; } }

    [SerializeField]
    private Image charIcon = null;

    public Action<CharStats> OnCycle { get; set; }

    private CircularLinkedList<CharStats> characters;

    void Start()
    {
        //characters = new CircularLinkedList<CharStats>(GameManager.Instance.GetActiveChars());
        //charIcon.sprite = Current.CharImage;
        //OnCycle?.Invoke(Current);
    }

    public void LoadElements(CharStats[] elements)
    {
        characters = new CircularLinkedList<CharStats>(elements);
        charIcon.sprite = Current.CharImage;
        OnCycle?.Invoke(Current);
    }

    public void GoNext()
    {
        characters.NextPos();
        ProcessAfterCycle();
    }

    public void GoPrev()
    {
        characters.PrevPos();
        ProcessAfterCycle();
    }  
    
    private void ProcessAfterCycle()
    {
        charIcon.sprite = Current.CharImage;
        OnCycle?.Invoke(Current);
    }
}
