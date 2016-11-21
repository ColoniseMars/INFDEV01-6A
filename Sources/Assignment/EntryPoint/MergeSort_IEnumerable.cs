using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    public static partial class MergeSort
    {
        /// <summary>
        /// Sorts an IEnumerable of any type without changing original IEnumarable data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSort"></param>
        /// <param name="ABeforeB"></param>
        /// <returns>New sorted IEnumerable</returns>
        public static IEnumerable<T> Sort<T>(IEnumerable<T> toSort, Func<T, T, bool> ABeforeB)
        {
            return Sort(toSort.ToArray(), 0, toSort.Count()-1, ABeforeB);
        }
        /// <summary>
        /// Takes an array, a range and a func to sort an array. func describes if the first argument (lowest index original array) should go before the second argument (higher index orginal array).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSort"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="ABeforeB"></param>
        /// <returns>New sorted array.</returns>
        private static T[] Sort<T> (T[] toSort, int startIndex, int endIndex, Func<T, T, bool> ABeforeB)
        {
            if (startIndex < endIndex)
            {
                int middle = (endIndex - startIndex) / 2 + startIndex;
                T[] A, B;
                A = Sort(toSort, startIndex, middle, ABeforeB);
                B = Sort(toSort, middle + 1, endIndex, ABeforeB);
                return Merge(A, B, ABeforeB);
            }
            return new T[1] { toSort[startIndex] };
        }

        /// <summary>
        /// Merges two arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="ABeforeB"></param>
        /// <returns>New sorted array</returns>
        private static T[] Merge<T>(T[] A, T[] B, Func<T, T, bool> ABeforeB)
        {
            T[] toReturn = new T[A.Length + B.Length];

            int Aindex, Bindex, ReturnIndex;
            Aindex = 0;
            Bindex = 0;
            ReturnIndex = 0;

            while(Aindex<A.Length && Bindex < B.Length)
            {
                if (ABeforeB(A[Aindex], B[Bindex]))
                {
                    toReturn[ReturnIndex] = A[Aindex];
                    Aindex++;
                }
                else
                {
                    toReturn[ReturnIndex] = B[Bindex];
                    Bindex++;
                }
                ReturnIndex++;
            }
            while (Aindex < A.Length)
            {
                toReturn[ReturnIndex] = A[Aindex];
                ReturnIndex++;
                Aindex++;
            }
            while (Bindex < B.Length)
            {
                toReturn[ReturnIndex] = B[Bindex];
                ReturnIndex++;
                Bindex++;
            }
            return toReturn;
        }
    }
}
