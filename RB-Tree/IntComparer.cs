#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;

namespace RB_Tree
{
    public class IntComparer : IComparer
    {
        public int Compare(object? key1, object? key2)
        {
            var keyInt1 = (int) key1;

            var keyInt2 = (int) key2;

            return keyInt1.CompareTo(keyInt2);
        }
    }
}