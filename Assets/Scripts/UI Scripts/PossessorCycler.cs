using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cycler
{
    public class PossessorCycler : MonoBehaviour, ICycler<ItemPossessor>
    {
        public ItemPossessor Current { get { return charList.current.value; } }

        [SerializeField]
        private Text possessorText = null;

        public Action<ItemPossessor> OnCycle { get; set; }

        private CircularLinkedList<ItemPossessor> charList;

        void Start()
        {
            charList = new CircularLinkedList<ItemPossessor>();
            PossessorSearcher.FillPossessorList(charList);
            possessorText.text = PossessorSearcher.GetPossessorName(Current);
        }

        public void GoNext()
        {
            string possText = "";

            do
            {
                charList.NextPos();
                possText = PossessorSearcher.GetPossessorName(Current);
            }
            while (possText.Equals(""));

            ProcessAfterCycle(possText);
        }

        public void GoPrev()
        {
            string possText = "";

            do
            {
                charList.PrevPos();
                possText = PossessorSearcher.GetPossessorName(Current);
            }
            while (possText.Equals(""));

            ProcessAfterCycle(possText);
        }

        private void ProcessAfterCycle(string possText)
        {
            possessorText.text = possText;
            OnCycle?.Invoke(Current);
        }

        public void LoadElements(ItemPossessor[] elements)
        { }
    }
}
