namespace RB_Tree
{
    public class Node<T,V>
    {
        public Color Color { get; set; }

        public readonly T Key;

        public V Value;

        public Node<T,V> LeftChild { get; set; }

        public Node<T,V> RightChild { get; set; }

        public Node<T,V> Parent { get; set; }
        
        public Node(T key,V value)
        {
            Key = key;

            Value = value;
        }
    }
}