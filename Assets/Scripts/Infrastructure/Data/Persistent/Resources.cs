using Plugins.Banks.Integer;

namespace Infrastructure.Data.Persistent
{
    public class Resources
    {
        public readonly IntegerBank Coins = new IntegerBank(0);
    }
}