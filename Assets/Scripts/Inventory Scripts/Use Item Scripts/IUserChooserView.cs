namespace RPG.Inventory
{
    public interface IUserChooserView
    {
        void Init();
        void Destruct();
        void ShowUserStat(int idx, string userName, int userStat, EntityStats.Attributes attr);
        void ShowUserStat(int idx, string userName, int curStat, int maxStat, EntityStats.Attributes attr);
        void UpdateRemaining(int nRemaining);
    }
}
