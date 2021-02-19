namespace RB_Tree
{
    public class IntegerComparer : IComparer<int>
    {
        public Comparison Compare(int num1, int num2)
        {
            if (num1 > num2)
            {
                return Comparison.More;
            }

            return num1 < num2 ? Comparison.Less : Comparison.Equals;
        }
    }
}