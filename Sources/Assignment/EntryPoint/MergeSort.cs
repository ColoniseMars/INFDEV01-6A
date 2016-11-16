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
        /// <summary>Sorts an IEnumerable according to a function.
        /// <para>Takes any IEnumerable and sorts it using mergesort. Returns a new IEnumerable</para>
        /// </summary>
        public static IEnumerable<T> Sort<T>(IEnumerable<T> toSort, Func<T, T, bool> ASmallerThanB)
        {
            return Sort(toSort.ToList(), ASmallerThanB);
        }
        /// <summary>Sorts a List according to a function.
        /// <para>Takes any List and sorts it using mergesort. Returns a new List</para>
        /// </summary>
        public static List<T> Sort<T>(List<T> toSort, Func<T, T, bool> ASmallerThanB)
        {
            if(toSort.Count > 1)
            {
                int middle = toSort.Count / 2;
                List<T> A, B;
                A = toSort.GetRange(0, middle);
                B = toSort.GetRange(middle, toSort.Count-middle);
                A = Sort(A, ASmallerThanB);
                B = Sort(B, ASmallerThanB);
                return Merge(A, B, ASmallerThanB);
            }
            return toSort;
        }
        /// <summary>Merges two lists in order according to a function.
        /// <para>Takes two lists and sorts it according to a function. Returns a new List</para>
        /// </summary>
        private static List<T> Merge<T>(List<T> A, List<T> B, Func<T, T, bool> ASmallerThanB)
        {
            List<T> toreturn = new List<T>();

            while (A.Count>0 && B.Count > 0)
            {
                if(ASmallerThanB(A[0], B[0]))
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
