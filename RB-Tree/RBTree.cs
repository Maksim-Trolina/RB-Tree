using System.Threading.Tasks;

namespace RB_Tree
{
    public class RBTree<T, V> : IMap<T, V>
    {
        private Node<T, V> root;

        public void Insert(T key, V value)
        {
            if (root == null)
            {
                root = new Node<T, V>(key, value) {Color = Color.Black};

                return;
            }

            /*if ()*/
        }

        public V Find(T key)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(T key)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public T[] GetKeys()
        {
            throw new System.NotImplementedException();
        }

        public V[] GetValues()
        {
            throw new System.NotImplementedException();
        }

        public void Print()
        {
            throw new System.NotImplementedException();
        }

        private void LeftTurn()
        {
        }

        private void RightTurn()
        {
        }

        /*private Node<T, V> Find(T key,V value)
        {
            var currentNode = root;

            while (currentNode!=null)
            {
                if(currentNode.Key)
            }
        }*/
    }
}