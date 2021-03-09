using System.Collections.Generic;

namespace RB_Tree
{
    public interface IMap<T,V>
    {
        void Insert(T key, V value);

        V Find(T key);

        int Count { get;}

        void Remove(T key);

        void Clear();

        List<T> GetKeys();

        List<V> GetValues();

        string Print();
    }
}