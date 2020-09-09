using Cycler;
using UnityEngine;

namespace RPG.Inventory
{
    public class EquipAction : InventoryAction
    {
        #region SERIALIZE FIELD
        [SerializeField]
        private GameObject characterCyclerObject = null;

        [SerializeField]
        private StatChangesView statChangesViewObject = null;
        #endregion

        private ICycler<CharStats> characterCycler;

        private IStatChangesView statChangesView;

        protected override void Awake()
        {
            base.Awake();
            characterCycler = characterCyclerObject.GetComponent<ICycler<CharStats>>();
            statChangesView = statChangesViewObject.GetComponent<IStatChangesView>();
        }

        public override void Invoke()
        {
            statChangesView.OnActivate += () => characterCycler.OnCycle += UpdateStats;
            statChangesView.OnDeactivate += () => characterCycler.OnCycle -= UpdateStats;
            statChangesView.Activate();
        }
       
        private (int cur, int after)[] GetStatChanges(CharStats stats)
        {
            var changes = new (int, int)[6];
            var chosenWeapon = inventoryController.ChosenItemHolder.TheItem as Equipment;
            var afterChangeStat = chosenWeapon.GetLaterChangeStat(stats);

            changes[0] = (stats.Strength, afterChangeStat.strength);
            changes[1] = (stats.Defense, afterChangeStat.defense);
            changes[2] = (stats.Intellect, afterChangeStat.intellect);
            changes[3] = (stats.Vitality, afterChangeStat.vitality);
            changes[4] = (stats.Agility, afterChangeStat.agility);
            changes[5] = (stats.Luck, afterChangeStat.luck);

            return changes;
        }

        private void UpdateStats(CharStats stats)
        {
            statChangesView.Show(GetStatChanges(stats));
        }
    }
}
