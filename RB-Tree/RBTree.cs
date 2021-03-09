using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RB_Tree
{
    public class RbTree<T, V> : IMap<T, V>
    {
        private IComparer comparer;

        private Node<T, V> root;

        public int Count { get; private set; } = 0;

        public RbTree(IComparer comparer)
        {
            this.comparer = comparer;
        }

        public void Insert(T key, V value)
        {
            Count++;

            var newNode = new Node<T, V>(key, value);

            if (root == null)
            {
                newNode.Color = Color.Black;

                root = newNode;

                return;
            }

            var parent = Find(root, newNode.Key);

            newNode.Parent = parent;

            AddChild(parent,newNode);

            FixInsert(newNode);
        }

        private void AddChild(Node<T, V> parent, Node<T, V> child)
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

        public V Find(T key)
        {
            var node = Find(root, key);

            if (!NodeExist(node,key))
            {
                throw new Exception("Not found item with same key");
            }

            return node.Data;
        }

        private bool NodeExist(Node<T, V> node, T key)
        {
            return node != null && comparer.Compare(node.Key, key) == (int) ComparisonResult.Equal;
        }

        public void Remove(T key)
        {
            var node = Find(root, key);

            if (!NodeExist(node,key))
            {
                throw new Exception("Not found item with same key");
            }

            Count--;

            if (!LeftChildExist(node) && !RightChildExist(node))
            {
                RemoveWithZeroChild(node);

                return;
            }

            Node<T, V> nextNode = null;
            
            if (!LeftChildExist(node) || !RightChildExist(node))
            {
                RemoveWithOneChild(node);
            }
            else
            {
                RemoveWithTwoChild(node,ref nextNode);
            }

            if (nextNode!=null && nextNode!=node)
            {
                node.Color = nextNode.Color;

                node.Key = nextNode.Key;
            }

            if (IsBlack(nextNode))
            {
                FixRemove(nextNode);
            }

        }

        private bool IsBlack(Node<T, V> node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color == Color.Black;
        }

        private void RemoveWithZeroChild(Node<T, V> node)
        {
            if (IsRoot(node))
            {
                root = null;
            }
            else
            {
                if (IsLeftChild(node))
                {
                    node.Parent.Left = null;
                }
                else
                {
                    node.Parent.Right = null;
                }
            }
        }

        private void RemoveWithOneChild(Node<T, V> node)
        {
            if (IsRoot(node))
            {
                root = node.Left ?? node.Right;

                return;
            }
            if (!LeftChildExist(node))
            {
                if (IsLeftChild(node))
                {
                    node.Parent.Left = node.Left;
                }
                else
                {
                    node.Parent.Right = node.Left;
                }
            }
            else
            {
                if (IsLeftChild(node))
                {
                    node.Parent.Left = node.Right;
                }
                else
                {
                    node.Parent.Right = node.Right;
                }
            }
            
            
        }

        private bool IsRoot(Node<T, V> node)
        {
            return node == root;
        }

        private void RemoveWithTwoChild(Node<T, V> node,ref Node<T,V> nextNode)
        {
            nextNode = NextNode(node);
            
            if (!RightChildExist(node))
            {
                nextNode.Right.Parent = nextNode.Parent;
            }

            if (IsRoot(nextNode))
            {
                root = nextNode.Right;
            }
            else
            {
                var father = nextNode.Parent;

                if (IsLeftChild(nextNode))
                {
                    father.Left = nextNode.Right;
                }
                else
                {
                    father.Right = nextNode.Right;
                }
            }
        }

        public void Clear()
        {
            root = null;

            Count = 0;
        }

        public List<T> GetKeys()
        {
            var keys = new List<T>();

            AddKey(root, keys);

            return keys;
        }

        private void AddKey(Node<T, V> node, List<T> keys)
        {
            if (node == null)
            {
                return;
            }

            AddKey(node.Left, keys);

            keys.Add(node.Key);

            AddKey(node.Right, keys);
        }

        public List<V> GetValues()
        {
            var values = new List<V>();

            AddValue(root, values);

            return values;
        }

        private void AddValue(Node<T, V> node, List<V> values)
        {
            if (node == null)
            {
                return;
            }

            AddValue(node.Left, values);

            values.Add(node.Data);

            AddValue(node.Right, values);
        }

        public string Print()
        {
            var keys = GetKeys();

            var values = GetValues();

            var output = "";

            for (var i = 0; i < Count; i++)
            {
                output += $"key = {keys[i]} value = {values[i]}\n";
            }

            Console.WriteLine(output);

            return output;
        }

        private void FixRemove(Node<T, V> node)
        {
            while (IsBlack(node) && !IsRoot(node))
            {
                if (IsLeftChild(node))
                {
                    var brother = GetBrother(node);

                    if (brother == null)
                    {
                        break;
                    }

                    if (!IsBlack(brother))
                    {
                        brother.Color = Color.Black;

                        node.Parent.Color = Color.Red;
                        
                        LeftRotate(node.Parent);
                    }

                    if (IsLeftChildBlack(brother) && IsRightChildBlack(brother))
                    {
                        brother.Color = Color.Red;
                    }
                    else
                    {
                        if (IsRightChildBlack(brother))
                        {
                            brother.Left.Color = Color.Black;

                            brother.Color = Color.Red;
                            
                            RightRotate(brother);
                        }

                        brother.Color = node.Parent.Color;

                        node.Parent.Color = Color.Black;

                        brother.Right.Color = Color.Black;
                        
                        LeftRotate(node.Parent);

                        node = root;
                    }
                }
                else
                {
                    var brother = GetBrother(node);
                    
                    if (brother == null)
                    {
                        break;
                    }

                    if (!IsBlack(brother))
                    {
                        brother.Color = Color.Black;

                        node.Parent.Color = Color.Red;
                        
                        RightRotate(node.Parent);
                    }

                    if (IsLeftChildBlack(brother) && IsRightChildBlack(brother))
                    {
                        brother.Color = Color.Red;
                    }
                    else
                    {
                        if (IsLeftChildBlack(brother))
                        {
                            brother.Right.Color = Color.Black;

                            brother.Color = Color.Red;
                            
                            LeftRotate(brother);
                        }

                        brother = node.Parent;

                        node.Parent.Color = Color.Black;

                        brother.Left.Color = Color.Black;
                        
                        RightRotate(node.Parent);

                        node = root;
                    }
                }
            }

            node.Color = Color.Black;

            root.Color = Color.Black;
        }

        private bool IsLeftChildBlack(Node<T, V> node)
        {
            return IsBlack(node.Left);
        }

        private bool IsRightChildBlack(Node<T, V> node)
        {
            return IsBlack(node.Left);
        }
        
        private bool LeftChildExist(Node<T, V> node)
        {
            return node?.Left != null;
        }

        private bool RightChildExist(Node<T, V> node)
        {
            return node?.Right != null;
        }

        private bool IsLeftChild(Node<T, V> node)
        {
            if (node.Parent == null)
            {
                return false;
            }

            return node.Parent.Left == node;
        }

        private Node<T, V> GetBrother(Node<T, V> node)
        {
            if (IsLeftChild(node))
            {
                return node.Parent.Right;
            }

            if (IsRightChild(node))
            {
                return node.Parent.Left;
            }

            return null;
        }

        private bool IsRightChild(Node<T, V> node)
        {
            if (node.Parent == null)
            {
                return false;
            }

            return node.Parent.Right == node;
        }

        private Node<T, V> NextNode(Node<T, V> node)
        {
            var child = node.Right;

            while (child.Left!=null)
            {
                child = child.Left;
            }

            return child;
        }
    
        private void FixInsert(Node<T, V> newNode)
        {
            var father = newNode.Parent;

            while (!IsBlack(father))
            {
                var grandFather = father.Parent;

                if (IsLeftChild(father))
                {
                    var uncle = grandFather.Right;

                    if (uncle != null)
                    {
                        if (!IsBlack(uncle))
                        {
                            father.Color = Color.Black;

                            uncle.Color = Color.Black;

                            grandFather.Color = Color.Red;

                            newNode = grandFather;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (IsRightChild(newNode))
                        {
                            newNode = father;

                            LeftRotate(newNode);
                        }

                        father.Color = Color.Black;

                        grandFather.Color = Color.Red;

                        RightRotate(grandFather);
                    }
                }
                else
                {
                    var uncle = grandFather.Left;

                    if (uncle != null)
                    {
                        if (!IsBlack(uncle))
                        {
                            father.Color = Color.Black;

                            uncle.Color = Color.Black;

                            grandFather.Color = Color.Red;

                            newNode = grandFather;
                        }
                    }
                    else
                    {
                        if (IsLeftChild(newNode))
                        {
                            newNode = father;

                            RightRotate(newNode);
                        }

                        father.Color = Color.Black;

                        grandFather.Color = Color.Red;

                        LeftRotate(grandFather);
                    }
                }
            }

            root.Color = Color.Black;
        }

        private void LeftRotate(Node<T, V> node)
        {
            var isRoot = IsRoot(node);

            var pivot = node.Right;

            pivot.Parent = node.Parent;

            if (node.Parent != null)
            {
                if (IsLeftChild(node))
                {
                    node.Parent.Left = pivot;
                }
                else
                {
                    node.Parent.Right = pivot;
                }
            }

            node.Right = pivot.Left;

            if (LeftChildExist(pivot))
            {
                pivot.Left.Parent = node;
            }

            node.Parent = pivot;

            pivot.Left = node;

            if (isRoot)
            {
                root = pivot;
            }
        }

        private void RightRotate(Node<T, V> node)
        {
            var isRoot = IsRoot(node);

            var pivot = node.Left;

            pivot.Parent = node.Parent;

            if (node.Parent != null)
            {
                if (IsLeftChild(node))
                {
                    node.Parent.Left = pivot;
                }
                else
                {
                    node.Parent.Right = pivot;
                }
            }

            node.Left = pivot.Right;

            if (RightChildExist(pivot))
            {
                pivot.Right.Parent = node;
            }

            node.Parent = pivot;

            pivot.Right = node;

            if (isRoot)
            {
                root = pivot;
            }
        }

        private Node<T, V> Find(Node<T, V> current, T key)
        {
            var compRes = (ComparisonResult) comparer.Compare(current.Key, key);

            return compRes switch
            {
                ComparisonResult.Less => current.Right == null ? current : Find(current.Right, key),
                ComparisonResult.Equal => current,
                ComparisonResult.More => current.Left == null ? current : Find(current.Left, key),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}