using System;
using System.Collections;

namespace RB_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IComparer comparer = new StrComparer();

                IMap<string, int> map = new RbTree<string, int>(comparer);

                Console.Write("How many items to insert: ");

                var countInsert = Convert.ToInt32(Console.ReadLine());

                for (var i = 0; i < countInsert; i++)
                {
                    Console.Write("Write key: ");

                    var key = Console.ReadLine();

                    Console.Write("Write value: ");

                    var value = Convert.ToInt32(Console.ReadLine());

                    map.Insert(key, value);
                }

                Console.Write("How many items to remove: ");

                var countRemove = Convert.ToInt32(Console.ReadLine());

                for (var i = 0; i < countRemove; i++)
                {
                    Console.Write("Write key: ");

                    var key = Console.ReadLine();

                    map.Remove(key);
                }

                Console.Write("How many items to find: ");

                var countFind = Convert.ToInt32(Console.ReadLine());

                for (var i = 0; i < countFind; i++)
                {
                    Console.Write("Write key: ");

                    var key = Console.ReadLine();

                    var value = map.Find(key);

                    Console.WriteLine($"Value: {value}");
                }

                Console.WriteLine("Output of all remaining items:");

                map.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}