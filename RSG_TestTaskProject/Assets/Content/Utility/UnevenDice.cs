using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Utility.Randomizer
{
    /// <summary>
    /// Статический класс для реализации алгоритмов выборки случайных элементов из списка заданных значений вероятности.
    /// Алгоритм "бросание случайного числа на отрезки".
    /// Берется список значений шансов некоторого набора событий - отрезков. Алгоритмом <see cref="UnityEngine.Random.Range"/> получаем случайное число
    /// от ноля до суммы всех "длинн отрезков" (значений шансов).
    /// Проходом по списку находим "отрезок" на который попало сгенерированное случайное число и возвращаем его индекс или ассоциированный с ним обьект(в случае подачи на вход ассоциативного списка).
    /// </summary>
    public static class UnevenDice
    {
        /// <summary>
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Список отрезков с вероятностью</param>
        /// <returns>Индекс отрезка</returns>
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
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Список отрезков с вероятностью</param>
        /// <returns>Индекс отрезка</returns>
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
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Словарь предмет-вероятность</param>
        /// <returns>Элемент словаря вероятностей</returns>
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
        /// Бросание вероятности на отрезки.
        /// </summary>
        /// <param name="chanseList">Словарь предмет-вероятность</param>
        /// <returns>Элемент словаря вероятностей</returns>
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
