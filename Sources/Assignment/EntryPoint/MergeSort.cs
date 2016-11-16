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
        public static IEnumerable<T> Sort<T>(IEnumerable<T> toSort, Func<T, float> CompareFunction)
        {
            return Sort(toSort.ToList<T>(), CompareFunction);
        }
        public static List<T> Sort<T>(List<T> toSort, Func<T, float> CompareFunction)
        {
            if(toSort.Count > 1)
            {
                int middle = toSort.Count / 2;
                List<T> A, B;
                A = toSort.GetRange(0, middle);
                B = toSort.GetRange(middle, toSort.Count-middle);
                A = Sort(A, CompareFunction);
                B = Sort(B, CompareFunction);
                return Merge(A, B, CompareFunction);
            }
            return toSort;
        }

        private static List<T> Merge<T>(List<T> A, List<T> B, Func<T, float> CompareFunction)
        {
            List<T> toreturn = new List<T>();

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
