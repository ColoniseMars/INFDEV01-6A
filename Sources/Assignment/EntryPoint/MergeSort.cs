using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public static class MergeSort
    {
        public static IEnumerable<Vector2> Sort(IEnumerable<Vector2> toSort, Func<Vector2, float> CompareFunction)
        {
            return Sort(toSort.ToList<Vector2>(), CompareFunction);
        }
        public static List<Vector2> Sort(List<Vector2> toSort, Func<Vector2, float> CompareFunction)
        {
            if(toSort.Count > 1)
            {
                int middle = toSort.Count / 2;
                List<Vector2> A, B;
                A = toSort.GetRange(0, middle);
                B = toSort.GetRange(middle, toSort.Count-middle);
                A = Sort(A, CompareFunction);
                B = Sort(B, CompareFunction);
                return Merge(A, B, CompareFunction);
            }
            return toSort;
        }

        private static List<Vector2> Merge(List<Vector2> A, List<Vector2> B, Func<Vector2, float> CompareFunction)
        {
            List<Vector2> toreturn = new List<Vector2>();

            while (A.Count>0 && B.Count > 0)
            {
                if(CompareFunction(A[0]) < CompareFunction(B[0]))
                {
                    toreturn.Add(A[0]);
                    A.RemoveAt(0);
                }
                else
                {
                    toreturn.Add(B[0]);
                    B.RemoveAt(0);
                }
            }
            while (A.Count > 0)
            {
                toreturn.Add(A[0]);
                A.RemoveAt(0);
            }
            while (B.Count > 0)
            {
                toreturn.Add(B[0]);
                B.RemoveAt(0);
            }
            return toreturn;
        }
    }
}
