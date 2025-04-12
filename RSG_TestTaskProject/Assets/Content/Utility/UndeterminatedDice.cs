using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Randomizer
{
    public class UndeterminatedDice
    {
        public CryptoRNGWrapper Random { get; private set; }
        public UndeterminatedDice()
        {
            Random = new CryptoRNGWrapper();
        }
        /// <summary>
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Список отрезков с вероятностью</param>
        /// <returns>Индекс отрезка</returns>
        public int Throw(IList<float> chanseList)
        {
            var rand = Random.NextFloat(0, chanseList.Sum());
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
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Список отрезков с вероятностью</param>
        /// <returns>Индекс отрезка</returns>
        public int Throw(IList<int> chanseList)
        {
            var rand = Random.NextFloat(0, chanseList.Sum());
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
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Словарь предмет-вероятность</param>
        /// <returns>Элемент словаря вероятностей</returns>
        public KeyValuePair<T, float> Throw<T>(IEnumerable<KeyValuePair<T, float>> chanseDict)
        {
            var rand = Random.NextFloat(0, chanseDict.Sum(x => x.Value));
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
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Словарь предмет-вероятность</param>
        /// <returns>Элемент словаря вероятностей</returns>
        public KeyValuePair<T, int> Throw<T>(IEnumerable<KeyValuePair<T, int>> chanseDict)
        {
            var rand = Random.NextFloat(0, chanseDict.Sum(x => x.Value));
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
        public int Throw(int min, int max)
        {
            return Random.NextInt(min, max);
        }

        /// <summary>         
        /// Return a random int within [minInclusive..maxExclusive).
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public int Throw(Vector2Int vector)
        {
            return Random.NextInt(vector.x, vector.y);
        }

        /// <summary>
        /// Throw true/false by a chance
        /// </summary>
        /// <param name="probability">Chance of positive out [0-1]f </param>
        /// <returns></returns>
        public bool Throw(float probability)
        {
            return UnityEngine.Random.value <= probability;
        }
    }
}
