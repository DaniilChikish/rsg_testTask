using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Utility.Randomizer
{
    /// <summary>
    /// Static class for implementing algorithms for selecting random elements from a list of given probability values.
    /// Algorithm for "throwing a random number on segments".
    /// A list of probability values ​​for a certain set of events - segments is taken. Using the <see cref="UnityEngine.Random.Range"/> algorithm, we obtain a random number
    /// from zero to the sum of all "segment lengths" (chance values).
    /// By traversing the list, we find the "segment" on which the generated random number falls and return its index or the object associated with it (in the case of an associative list being fed to the input).
    /// </summary>
    public static class UnevenDice
    {
        /// <summary>
        /// Throwing probabilities on segments.
        /// </summary>
        /// <param name="chanseList">List of segments with probability</param>
        /// <returns>Segment index</returns>
        public static int Throw(IList<float> chanseList)
        {
            var rand = UnityEngine.Random.Range(0, chanseList.Sum());
            var summ = 0f;
            for (int i = 0; i < chanseList.Count; i++)
            {
                float entry = chanseList[i];
                if (rand >= summ && rand < summ + entry)
                {
                    return i;
                }
                else
                {
                    summ += entry;
                }
            }
            throw new IndexOutOfRangeException("Random number is not included in any segment.");
        }

        /// <summary>
        /// Throwing probabilities on segments.
        /// </summary>
        /// <param name="chanseList">List of segments with probability</param>
        /// <returns>Segment index</returns>
        public static int Throw(IList<int> chanseList)
        {
            var rand = UnityEngine.Random.Range(0, chanseList.Sum());
            var summ = 0f;
            for (int i = 0; i < chanseList.Count; i++)
            {
                int entry = chanseList[i];
                if (rand >= summ && rand < summ + entry)
                {
                    return i;
                }
                else
                {
                    summ += entry;
                }
            }
            throw new IndexOutOfRangeException("Random number is not included in any segment.");
        }

        /// <summary>
        /// Throwing probabilities on segments.
        /// </summary>
        /// <param name="chanseList">Dictionary subject-probability</param>
        /// <returns>Probability dictionary element</returns>
        public static KeyValuePair<T, float> Throw<T>(IEnumerable<KeyValuePair<T, float>> chanseDict)
        {
            var rand = UnityEngine.Random.Range(0, chanseDict.Sum(x => x.Value));
            var summ = 0f;
            foreach (var entry in chanseDict)
            {
                if (rand >= summ && rand < summ + entry.Value)
                {
                    return entry;
                }
                else
                {
                    summ += entry.Value;
                }
            }
            throw new IndexOutOfRangeException("Random number is not included in any segment.");
        }

        /// <summary>
        /// Throwing probabilities on segments.
        /// </summary>
        /// <param name="chanseList">Dictionary subject-probability</param>
        /// <returns>Probability dictionary element</returns>
        public static KeyValuePair<T, int> Throw<T>(IEnumerable<KeyValuePair<T, int>> chanseDict)
        {
            var rand = UnityEngine.Random.Range(0, chanseDict.Sum(x => x.Value));
            var summ = 0f;
            foreach (var entry in chanseDict)
            {
                if (rand >= summ && rand < summ + entry.Value)
                {
                    return entry;
                }
                else
                {
                    summ += entry.Value;
                }
            }
            throw new IndexOutOfRangeException("Random number is not included in any segment.");
        }

        /// <summary>
        /// Return a random int within [min..max).
        /// </summary>
        /// <param name="min">Inclusive</param>
        /// <param name="max">Exclusive</param>
        /// <returns></returns>
        public static int Throw(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>         
        /// Return a random int within [minInclusive..maxExclusive).
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static int Throw(Vector2Int vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }

        /// <summary>
        /// Throw true/false by a chance
        /// </summary>
        /// <param name="probability">Chance of positive out [0-1]f </param>
        /// <returns></returns>
        public static bool Throw(float probability)
        {
            return UnityEngine.Random.value <= probability;
        }
    }

    public static class RandomExtension
    {
        public static float RandomRange(this Vector2 vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }
        public static int RandomRange(this Vector2Int vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }
    }
}
