using Plugins.Banks.Integer;
using UniRx;

namespace Infrastructure.Data.Persistent
{
    public class PlayerData
    {
        public readonly IntegerBank Coins = new IntegerBank(0);
        public readonly IntReactiveProperty SelectedLevelIndex = new IntReactiveProperty(1);
    }
}