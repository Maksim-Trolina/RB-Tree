namespace RB_Tree
{
    public interface IComparer<T>
    {
        public Comparison Compare(T obj1, T obj2);
    }
}