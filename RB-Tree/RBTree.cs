using System.Threading.Tasks;

namespace RB_Tree
{
    public class RBTree<T,V> : IMap<T, V>
    {
        private Node<T, V> root;

        private IComparer<T> comparer;

        public RBTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public void Insert(T key, V value)
        {
            var newNode = new Node<T,V>(key,value);

            var parent = GetParent(newNode.Key);

            if (parent == null)
            {
                newNode.Color = Color.Black;

                root = newNode;
                
                return;
            }
            
            newNode.Color = Color.Red;

            newNode.Parent = parent;
            
            SetChild(parent,newNode);
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

        private Node<T, V> GetParent(T key)
        {
            var currentNode = root;

            while (currentNode != null)
            {
                var comparisionResult = comparer.Compare(key, currentNode.Key);

                switch (comparisionResult)
                {
                    case Comparison.Less:
                        if (currentNode.LeftChild == null)
                        {
                            return currentNode;
                        }

                        currentNode = currentNode.LeftChild;
                        break;
                    case Comparison.Equals:
                        return currentNode.Parent;
                    case Comparison.More:
                        if (currentNode.RightChild == null)
                        {
                            return currentNode;
                        }

                        currentNode = currentNode.RightChild;
                        break;
                }
            }

            return null;
        }

        private void SetChild(Node<T,V> parent,Node<T,V> child)
        {
            var comparisionResult = comparer.Compare(parent.Key,child.Key);

            switch (comparisionResult)
            {
                case Comparison.Less:
                    parent.LeftChild = child;
                    break;
                case Comparison.More:
                    parent.RightChild = child;
                    break;
                case Comparison.Equals:
                    parent = child;
                    break;
            }
        }

     
        
    }
}