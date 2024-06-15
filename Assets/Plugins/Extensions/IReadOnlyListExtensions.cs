using System.Collections.Generic;

namespace Plugins.Extensions
{
    public static class ReadOnlyListExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T element)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(element))
                    return i;
            }

            return -1;
        }
    }
}