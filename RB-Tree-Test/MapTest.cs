using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RB_Tree;

namespace RB_Tree_Test
{
    public class Tests
    {
        [Test]
        public void FindIntTest_InsertSixItems_GetCorrectValue()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(33,5);
            
            map.Insert(34,1);
            
            map.Insert(32,9);
            
            map.Insert(30,6);
            
            map.Insert(36,7);
            
            map.Insert(10,10);

            var actual = map.Find(30);

            var expected = 6;
            
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void RemoveIntTest_InsertThreeItemsAndRemoveTwoItems_FindOneItem()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(3,5);
            
            map.Insert(34,1);
            
            map.Insert(32,9);
            
            map.Remove(3);
            
            map.Remove(32);

            var actual = map.Find(34);

            var expected = 1;
            
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void RemoveIntTest_InsertSevenItemsAndRemoveFourItems_FindAllItems()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(3,5);
            
            map.Insert(34,1);
            
            map.Insert(32,9);
            
            map.Insert(5,189);
            
            map.Insert(56,22);
            
            map.Insert(12,56);
            
            map.Insert(44,88);
            
            map.Remove(32);
            
            map.Remove(12);
            
            map.Remove(56);
            
            map.Remove(3);

            var actual = map.GetKeys();

            var expected = new List<int> {5, 34,44};
            
            Assert.IsTrue(AreEqualList(actual,expected,comparer));
        }

        [Test]
        public void RemoveIntTest_InsertSixItemsAndRemoveTheeItems_FindAllItems()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(3,5);
            
            map.Insert(34,1);
            
            map.Insert(32,9);
            
            map.Insert(5,189);
            
            map.Insert(56,22);

            map.Insert(44,88);
            
            map.Remove(44);

            map.Remove(5);
            
            map.Remove(3);
            
            var actual = map.GetKeys();

            var expected = new List<int> {32,34,56};
            
            Assert.IsTrue(AreEqualList(actual,expected,comparer));
        }

        [Test]
        public void FindIntTest_TryFindNoExistItem_Exception()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(4,45);
            
            map.Insert(74,21);
            
            map.Insert(31,90);
            
            map.Remove(74);

            Assert.Throws<Exception>(() => map.Find(74));
        }
        
        [Test]
        public void RemoveIntTest_TryRemoveNoExistItem_Exception()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(4,45);
            
            map.Insert(74,21);
            
            map.Insert(31,90);

            Assert.Throws<Exception>(() => map.Remove(75));
        }
        

        [Test]
        public void InsertIntTest_InsertThreeItems_CorrectInsert()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(1,4);
            
            map.Insert(5,3);
            
            map.Insert(9,8);

            var actualCount = map.Count;

            var expectedCount = 3;
            
            Assert.AreEqual(expectedCount,actualCount);
        }

        [Test]
        public void ClearIntTest_InsertTwoItemsAndClear_EmptyMap()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(2,55);
            
            map.Insert(59,43);
            
            map.Clear();

            var actualCount = map.Count;

            var expectedCount = 0;
            
            Assert.AreEqual(expectedCount,actualCount);
        }

        [Test]
        public void GetKeysIntTest_InsertThreeItems_GetThreeKeys()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(3,155);
            
            map.Insert(594,423);
            
            map.Insert(66,777);

            var actualKeys = map.GetKeys();

            var expectedKeys = new List<int> {3, 66, 594};
            
            Assert.IsTrue(AreEqualList(expectedKeys,actualKeys,comparer));
        }

        [Test]
        public void GetValuesIntTest_InsertThreeItems_GetTheeValues()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(3,155);
            
            map.Insert(594,423);
            
            map.Insert(66,777);

            var actualValues = map.GetValues();

            var expectedValues = new List<int> {155, 777, 423};
            
            Assert.IsTrue(AreEqualList(expectedValues,actualValues,comparer));
        }

        [Test]
        public void PrintIntTest_InsertThreeItems_CorrectPrint()
        {
            IComparer comparer = new IntComparer();
            
            IMap<int,int> map = new RbTree<int, int>(comparer);
            
            map.Insert(3,155);
            
            map.Insert(594,423);
            
            map.Insert(66,777);

            var actualOutput = map.Print();

            var expectedOutput = "key = 3 value = 155\nkey = 66 value = 777\nkey = 594 value = 423\n";
            
            Assert.AreEqual(expectedOutput,actualOutput);
        }

        [Test]
        public void FindStrTest_InsertSixItems_GetCorrectValue()
        {
            IComparer comparer = new StrComparer();
            
            IMap<string,int> map = new RbTree<string, int>(comparer);
            
            map.Insert("first",5);
            
            map.Insert("second",1);
            
            map.Insert("third",9);
            
            map.Insert("fourth",6);
            
            map.Insert("fifth",7);
            
            map.Insert("sixth",10);

            var actual = map.Find("fourth");

            var expected = 6;
            
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void InsertStrTest_InsertThreeItems_CorrectInsert()
        {
            IComparer comparer = new StrComparer();
            
            IMap<string,int> map = new RbTree<string, int>(comparer);
            
            map.Insert("first",4);
            
            map.Insert("second",3);
            
            map.Insert("third",8);

            var actualCount = map.Count;

            var expectedCount = 3;
            
            Assert.AreEqual(expectedCount,actualCount);
        }

        [Test]
        public void ClearStrTest_InsertTwoItemsAndClear_EmptyMap()
        {
            IComparer comparer = new StrComparer();
            
            IMap<string,int> map = new RbTree<string, int>(comparer);
            
            map.Insert("first",55);
            
            map.Insert("second",43);
            
            map.Clear();

            var actualCount = map.Count;

            var expectedCount = 0;
            
            Assert.AreEqual(expectedCount,actualCount);
        }
        
        [Test]
        public void GetKeysStrTest_InsertThreeItems_GetThreeKeys()
        {
            IComparer comparer = new StrComparer();
            
            IMap<string,int> map = new RbTree<string, int>(comparer);
            
            map.Insert("as",155);
            
            map.Insert("fgh",423);
            
            map.Insert("cz3",777);

            var actualKeys = map.GetKeys();

            var expectedKeys = new List<string> { "as","cz3","fgh"};
            
            Assert.IsTrue(AreEqualList(expectedKeys,actualKeys,comparer));
        }
        
        [Test]
        public void GetValuesStrTest_InsertThreeItems_GetThreeKeys()
        {
            IComparer comparer = new StrComparer();
            
            IMap<string,int> map = new RbTree<string, int>(comparer);
            
            map.Insert("as",155);
            
            map.Insert("fgh",423);
            
            map.Insert("cz3",777);

            var actualKeys = map.GetValues();

            var expectedKeys = new List<int> { 155,777,423};
            
            comparer = new IntComparer();
            
            Assert.IsTrue(AreEqualList(expectedKeys,actualKeys,comparer));
        }
        
        [Test]
        public void PrintStrTest_InsertThreeItems_CorrectPrint()
        {
            IComparer comparer = new StrComparer();
            
            IMap<string,int> map = new RbTree<string, int>(comparer);
            
            map.Insert("as",155);
            
            map.Insert("fgh",423);
            
            map.Insert("cz3",777);

            var actualOutput = map.Print();

            var expectedOutput = "key = as value = 155\nkey = cz3 value = 777\nkey = fgh value = 423\n";
            
            Assert.AreEqual(expectedOutput,actualOutput);
        }
        
        private bool AreEqualList<T>(List<T> list1, List<T> list2,IComparer comparer)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (comparer.Compare(list1[i],list2[i]) != (int) ComparisonResult.Equal)
                {
                    return false;
                }
            }

            return true;
        }
        
    }
}