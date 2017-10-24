using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Playflock.Extension
{
    public static class ListExtPF
    {
        /// <summary>
        ///  [0]
        ///  [1]
        ///  [2]
        ///  ----- >
        ///  [1]
        ///  [2]
        ///  [new value]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ShiftLeft<T>(this List<T> list)
        {
            List<T> copy = new List<T>(list);
            list[list.Count - 1] = default(T);
            for (int i = list.Count - 2; i >= 0; i--)
            {
                list[i] = copy[i + 1];
            }
        }

        /// <summary>
        ///  [0]
        ///  [1]
        ///  [2]
        ///   ----- >
        ///  [new value]
        ///  [0]
        ///  [1]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ShiftRight<T>(this List<T> list)
        {
            List<T> copy = new List<T>(list);
            list[0] = default(T);
            for (int i = 1; i < list.Count; i++)
            {
                list[i] = copy[i - 1];
            }
        }

    }


}
