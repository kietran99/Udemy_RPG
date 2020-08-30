using System;

namespace RPG.Inventory
{
    public interface IUserChooserView
    {
        Func<int, (int, int)> OnItemUse { get; set; }

        void Init();
        void Destruct();
        void ShowUserStat(int idx, EntityStats.Attributes attr, string userName, int curStat, int maxStat);
        void UpdateRemaining(int nRemaining);
    }
}
