using UnityEngine;
using UnityEngine.UI;

namespace Cycler
{
    public interface ICycleObserver<T>
    {
        void OnCycle(T value);
    }

    public class CharCycler : MonoBehaviour
    {
        #region
        public PossessorSearcher.ItemPossessor CurrPos { get { return currPoss; } }
        #endregion

        [SerializeField]
        private Text possessorText = null;

        private ICycleObserver<PossessorSearcher.ItemPossessor> observer;

        private CircularLinkedList<PossessorSearcher.ItemPossessor> charList;

        private PossessorSearcher.ItemPossessor currPoss;

        public void Activate(ICycleObserver<PossessorSearcher.ItemPossessor> observer)
        {
            this.observer = observer;
            charList = new CircularLinkedList<PossessorSearcher.ItemPossessor>();
            PossessorSearcher.FillPossessorList(charList);
            currPoss = charList.current.value;
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

            currPoss = charList.current.value;
            possessorText.text = possText;

            observer.OnCycle(charList.current.value);
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

            currPoss = charList.current.value;
            possessorText.text = possText;

            observer.OnCycle(charList.current.value);
        }
    }
}
