using System;
using System.Collections.Generic;

namespace Sudoku.Algorithms
{
    public static class ShuffleEx
    {
        private static readonly Random random = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                // get index to switch with
                int k = random.Next(i, list.Count);

                if (i != k)
                {
                    // switch list[k] <--> list[i]
                    T value = list[k];
                    list[k] = list[i];
                    list[i] = value;
                }
            }

            return list;
        }

        public static T ChooseRandom<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentException("list must not be null or empty");
            }

            int index = random.Next(0, list.Count);
            return list[index];
        }
    }
}
