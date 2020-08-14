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
        public ItemPossessor CurrPos { get { return currPoss; } }
        #endregion

        [SerializeField]
        private Text possessorText = null;

        private ICycleObserver<ItemPossessor> observer;

        private CircularLinkedList<ItemPossessor> charList;

        private ItemPossessor currPoss;

        public void Activate(ICycleObserver<ItemPossessor> observer)
        {
            this.observer = observer;
            charList = new CircularLinkedList<ItemPossessor>();
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
