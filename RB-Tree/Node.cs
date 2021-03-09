namespace RB_Tree
{
    public class Node<T, V>
    {
        public Color Color { get; set; } = Color.Red;
        public Node<T, V> Left { get; set; }
        public Node<T, V> Right { get; set; }

        public Node<T, V> Parent { get; set; }

        public T Key { get; set; }

        public V Data { get; set; }

        public Node(T key, V data)
        {
            Key = key;

            Data = data;
        }
    }
}