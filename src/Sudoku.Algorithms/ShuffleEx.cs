using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Algorithms
{
    public static class ShuffleEx
    {
        private static readonly Random random = new Random();

        public static List<T> Shuffle<T>(this IEnumerable<T> items)
        {
            List<T> results = items.ToList();

            for (int i = 0; i < results.Count - 1; i++)
            {
                // get index to switch with
                int k = random.Next(i, results.Count);

                if (i != k)
                {
                    // switch results[k] <--> results[i]
                    T value = results[k];
                    results[k] = results[i];
                    results[i] = value;
                }
            }

            return results;
        }

        public static T ChooseRandomOrDefault<T>(this IEnumerable<T> list)
        {
            if (list?.Count() == 0) { return default(T); }
            return list.ChooseRandom();
        }

        public static T ChooseRandom<T>(this IEnumerable<T> list)
        {
            int count = list != null ? Enumerable.Count(list) : 0;

            if (count == 0)
            {
                throw new ArgumentException("list must not be null or empty");
            }

            int index = random.Next(0, count);
            T item = Enumerable.ElementAt(list, index);

            return item;
        }

        public static List<T> ChooseRandom<T>(this IEnumerable<T> list, int range)
        {
            int count = list != null ? Enumerable.Count(list) : 0;

            if (count == 0)
            {
                throw new ArgumentException("list must not be null or empty");
            }
            
            return list.Shuffle().Take(range > count ? count : range).ToList();
        }
    }
}
