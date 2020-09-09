using UnityEngine;
using Functional;
using System;
using Cycler;

namespace RPG.Inventory
{
    public class StatChangesView : MonoBehaviour, IStatChangesView
    {
        #region SERIALIZE FIELD
        [SerializeField]
        private StatChange[] statChanges = null;

        [SerializeField]
        [ColorUsage(true)]
        private Color statIncColor = Color.blue, statDecColor = Color.red, statUnchangeColor = Color.white;
        #endregion

        #region DELEGATES
        public Action OnActivate { get; set; }
        public Action OnDeactivate { get; set; }
        #endregion             

        private void Update()
        {
            if (!Input.GetKeyDown(KeyboardControl.General.Exit)) return;

            Deactivate();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            OnActivate?.Invoke();
        }

        public void Show((int cur, int after)[] changes)
        {
            HOF.Map((change, idx) =>
            statChanges[idx].Show(change.cur.ToString(), change.after.ToString(), GetChangeColor(change.cur, change.after)), changes);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            OnDeactivate?.Invoke();
        }

        private Color GetChangeColor(int cur, int after)
        {
            if (cur < after) return statIncColor;
            else if (cur > after) return statDecColor;
            else return statUnchangeColor;
        }

        public void Confirm()
        {
            Deactivate();
        }

        public void Cancel()
        {
            Deactivate();
        }
    }
}
