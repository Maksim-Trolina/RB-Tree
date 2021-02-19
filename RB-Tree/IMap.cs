namespace RB_Tree
{
    public interface IMap<T,V>
    {
        void Insert(T key, V value);

        V Find(T key);

        void Remove(T key);

        void Clear();

        T[] GetKeys();

        V[] GetValues();

        void Print();
    }
}