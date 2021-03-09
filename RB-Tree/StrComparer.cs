using System.Collections;

namespace RB_Tree
{
    public class StrComparer : IComparer
    {
        public int Compare(object? key1, object? key2)
        {
            var keyStr1 = (string) key1;

            var keyStr2 = (string) key2;

            return keyStr1.CompareTo(keyStr2);
        }
    }
}