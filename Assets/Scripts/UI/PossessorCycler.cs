using Cycler;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class PossessorCycler : MonoBehaviour, ICycler<ItemOwner>
    {
        public ItemOwner Current { get { return charList.current.value; } }

        [SerializeField]
        private Text possessorText = null;

        public Action<ItemOwner> OnCycle { get; set; }

        private CircularLinkedList<ItemOwner> charList;

        void Start()
        {
            charList = new CircularLinkedList<ItemOwner>();
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

        public void LoadElements(ItemOwner[] elements)
        { }
    }
}
