using System.Collections;

namespace RB_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
          IComparer comparer = new IntComparer();
          
          RbTree<int,int> map = new RbTree<int, int>(comparer);
          
          map.Insert(3,155);
            
          map.Insert(594,423);
            
          map.Insert(66,777);
          
          map.Print();
        }
    }
    
}