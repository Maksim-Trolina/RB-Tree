using System;
using System.Collections;
using System.Collections.Generic;

namespace RB_Tree
{
    public class RbTree<T, V> : IMap<T, V>
    {
        private IComparer comparer;

        private Node<T, V> root;

        public int Count { get; private set; }

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

            parent.AddChild(newNode, comparer);

            FixInsert(newNode);
        }


        public V Find(T key)
        {
            var node = Find(root, key);

            if (!IsNodeExist(node, key))
            {
                throw new Exception("Not found item with same key");
            }

            return node.Data;
        }

        public void Remove(T key)
        {
            var node = Find(root, key);

            if (!IsNodeExist(node, key))
            {
                throw new Exception("Not found item with same key");
            }

            Count--;

            if (!node.IsLeftChildExist() && !node.IsRightChildExist())
            {
                RemoveWithZeroChild(node);

                return;
            }

            Node<T, V> nextNode = null;

            if (!node.IsLeftChildExist() || !node.IsRightChildExist())
            {
                RemoveWithOneChild(node);
            }
            else
            {
                RemoveWithTwoChild(node, ref nextNode);
            }

            if (nextNode != null && nextNode != node)
            {
                node.Color = nextNode.Color;

                node.Key = nextNode.Key;
            }

            if (nextNode.IsBlack())
            {
                FixRemove(nextNode);
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

        public List<V> GetValues()
        {
            var values = new List<V>();

            AddValue(root, values);

            return values;
        }

        public void Print()
        {
            var output = ToString();

            Console.WriteLine(output);
        }

        public override string ToString()
        {
            var keys = GetKeys();

            var values = GetValues();

            var output = "";

            for (var i = 0; i < Count; i++)
            {
                output += $"key = {keys[i]} value = {values[i]}\n";
            }

            return output;
        }

        private void RemoveWithZeroChild(Node<T, V> node)
        {
            if (IsRoot(node))
            {
                root = null;
            }
            else
            {
                if (node.IsLeftChild())
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

            if (!node.IsLeftChildExist())
            {
                if (node.IsLeftChild())
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
                if (node.IsLeftChild())
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

        private void RemoveWithTwoChild(Node<T, V> node, ref Node<T, V> nextNode)
        {
            nextNode = node.NextNode();

            if (!node.IsRightChildExist())
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

                if (nextNode.IsLeftChild())
                {
                    father.Left = nextNode.Right;
                }
                else
                {
                    father.Right = nextNode.Right;
                }
            }
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

        private void FixRemove(Node<T, V> node)
        {
            while (node.IsBlack() && !IsRoot(node))
            {
                if (node.IsLeftChild())
                {
                    var brother = node.GetBrother();

                    if (brother == null)
                    {
                        break;
                    }

                    if (!brother.IsBlack())
                    {
                        brother.Color = Color.Black;

                        node.Parent.Color = Color.Red;

                        LeftRotate(node.Parent);
                    }

                    if (brother.IsLeftChildBlack() && brother.IsRightChildBlack())
                    {
                        brother.Color = Color.Red;
                    }
                    else
                    {
                        if (brother.IsRightChildBlack())
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
                    var brother = node.GetBrother();

                    if (brother == null)
                    {
                        break;
                    }

                    if (!brother.IsBlack())
                    {
                        brother.Color = Color.Black;

                        node.Parent.Color = Color.Red;

                        RightRotate(node.Parent);
                    }

                    if (brother.IsLeftChildBlack() && brother.IsRightChildBlack())
                    {
                        brother.Color = Color.Red;
                    }
                    else
                    {
                        if (brother.IsLeftChildBlack())
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

        private void FixInsert(Node<T, V> newNode)
        {
            var father = newNode.Parent;

            while (!father.IsBlack())
            {
                var grandFather = father.Parent;

                if (father.IsLeftChild())
                {
                    var uncle = grandFather.Right;

                    if (uncle != null)
                    {
                        if (!uncle.IsBlack())
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
                        if (newNode.IsRightChild())
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
                        if (!uncle.IsBlack())
                        {
                            father.Color = Color.Black;

                            uncle.Color = Color.Black;

                            grandFather.Color = Color.Red;

                            newNode = grandFather;
                        }
                    }
                    else
                    {
                        if (newNode.IsLeftChild())
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
                if (node.IsLeftChild())
                {
                    node.Parent.Left = pivot;
                }
                else
                {
                    node.Parent.Right = pivot;
                }
            }

            node.Right = pivot.Left;

            if (pivot.IsLeftChildExist())
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
                if (node.IsLeftChild())
                {
                    node.Parent.Left = pivot;
                }
                else
                {
                    node.Parent.Right = pivot;
                }
            }

            node.Left = pivot.Right;

            if (pivot.IsRightChildExist())
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

        private bool IsNodeExist(Node<T, V> node, T key)
        {
            return node != null && comparer.Compare(node.Key, key) == (int) ComparisonResult.Equal;
        }
    }
}