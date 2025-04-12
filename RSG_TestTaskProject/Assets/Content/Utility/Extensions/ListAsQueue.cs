using System.Collections.Generic;

namespace Utility.Extensions
{
    public static class ListAsQueue
    {
        public static void Enqueue<T>(this List<T> list, T item)
        {
            list.Add(item);
        }

        public static T Dequeue<T>(this List<T> list)
        {
            var t = list[0];
            list.RemoveAt(0);
            return t;
        }

        public static T Peek<T>(this List<T> list)
        {
            return list[0];
        }
    }
}
