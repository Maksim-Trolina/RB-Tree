using System.Collections;

namespace RB_Tree
{
    public static class NodeExtension
    {
        public static bool IsLeftChildExist<T, V>(this Node<T, V> node)
        {
            return node?.Left != null;
        }

        public static bool IsRightChildExist<T, V>(this Node<T, V> node)
        {
            return node?.Right != null;
        }

        public static bool IsLeftChild<T, V>(this Node<T, V> node)
        {
            if (node.Parent == null)
            {
                return false;
            }

            return node.Parent.Left == node;
        }

        public static bool IsRightChild<T, V>(this Node<T, V> node)
        {
            if (node.Parent == null)
            {
                return false;
            }

            return node.Parent.Right == node;
        }

        public static Node<T, V> NextNode<T, V>(this Node<T, V> node)
        {
            var child = node.Right;

            while (child.Left != null)
            {
                child = child.Left;
            }

            return child;
        }

        public static bool IsBlack<T, V>(this Node<T, V> node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color == Color.Black;
        }

        public static Node<T, V> GetBrother<T, V>(this Node<T, V> node)
        {
            if (node.IsLeftChild())
            {
                return node.Parent.Right;
            }

            if (node.IsRightChild())
            {
                return node.Parent.Left;
            }

            return null;
        }

        public static bool IsLeftChildBlack<T, V>(this Node<T, V> node)
        {
            return node.Left.IsBlack();
        }

        public static bool IsRightChildBlack<T, V>(this Node<T, V> node)
        {
            return node.Right.IsBlack();
        }

        public static void AddChild<T, V>(this Node<T, V> parent, Node<T, V> child, IComparer comparer)
        {
            var compRes = (ComparisonResult) comparer.Compare(parent.Key, child.Key);

            if (compRes == ComparisonResult.Less)
            {
                parent.Right = child;
            }
            else
            {
                parent.Left = child;
            }
        }
    }
}