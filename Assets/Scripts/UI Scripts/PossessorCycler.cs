using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cycler
{
    public class PossessorCycler : MonoBehaviour, ICycler<ItemPossessor>
    {
        #region
        public ItemPossessor Current { get; set; }
        public Action<ItemPossessor> OnCycle { get ; set; }
        #endregion

        [SerializeField]
        private Text possessorText = null;

        private CircularLinkedList<ItemPossessor> charList;

        void Start()
        {
            charList = new CircularLinkedList<ItemPossessor>();
            PossessorSearcher.FillPossessorList(charList);
            Current = charList.current.value;
        }

        public void NextChar()
        {
            string possText = "";

            do
            {
                charList.NextPos();
                possText = PossessorSearcher.GetPossessorName(charList.current.value);
            }
            while (possText.Equals(""));

            ProcessPostCycle(possText);
        }

        public void PrevChar()
        {
            string possText = "";

            do
            {
                charList.PrevPos();
                possText = PossessorSearcher.GetPossessorName(charList.current.value);
            }
            while (possText.Equals(""));

            ProcessPostCycle(possText);
        }

        private void ProcessPostCycle(string possText)
        {
            Current = charList.current.value;
            possessorText.text = possText;
            OnCycle?.Invoke(charList.current.value);
        }
    }
}
