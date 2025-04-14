using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Extensions;

namespace Utility
{
    public static class ExtendedListOperation
    {
        /// <summary>
        /// First array is reshuffle of second array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsReshuffle<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return (first.Count() == second.Count() && ContainsSublist(first, second));
        }

        /// <summary>
        /// First list contains all items of second list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>True if first list contains all items of second list</returns>
        public static bool ContainsSublist<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (T item in second)
            {
                if (!first.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Return random enry of list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T SelectRandom<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count())];
        }

        /// <summary>
        /// Return random enry of list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T SelectRandom<T>(this IEnumerable<T> list)
        {
            List<T> buffer = ShuffleList(list);
            return buffer.FirstOrDefault();
        }

        /// <summary>
        /// Return random enry of list. Safe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> SelectRandom<T>(this IEnumerable<T> list, int count)
        {
            List<T> buffer = ShuffleList(list);
            return buffer.Take(count).ToList();
        }

        /// <summary>
        /// Return random sheffled list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> ShuffleList<T>(IEnumerable<T> inputList)
        {
            return inputList.OrderBy(x => UnityEngine.Random.Range(0f, 1f)).ToList();
        }
        public static List<T> ShuffleList<T>(IEnumerable<T> inputList, int seed)
        {
            List<T> output;
            UnityEngine.Random.InitState(seed);
            output = inputList.OrderBy(x => UnityEngine.Random.Range(0f, 1f)).ToList();
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            return output;
        }

        /// <summary>
        /// Convert string-int pairs to dictionary. Safe.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Dictionary<string, int> ToDictionary(this IEnumerable<KeyValuePair<string, int>> list)
        {
            if (list == null) 
                return null;
            var output = new Dictionary<string, int>();
            list.ForEach(x => output.SafeAdd(x.Key, x.Value));
            return output;
        }

        public static void SafeAdd(this IDictionary<string, int> dict, string key, int value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] += value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        public static void SafeAdd(this IDictionary<string, int> dict, KeyValuePair<string, int> pair)
        {
            SafeAdd(dict, pair.Key, pair.Value);
        }

        public static void SafeAddRange(this IDictionary<string, int> dict, IEnumerable<KeyValuePair<string, int>> list)
        {
            foreach (var entry in list)
                dict.SafeAdd(entry.Key, entry.Value);
        }
        /// <summary>
        /// Convert key-value pairs to dictionary. Unsafe!
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            return list.ToDictionary(x => x.Key, x => x.Value);
        }
        /// <summary>
        /// Convert key-value pairs to dictionary with boxing cast. Unsafe! 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="InTValue"></typeparam>
        /// <typeparam name="OutTValue"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Dictionary<TKey, OutTValue> ToDictionary<TKey, InTValue, OutTValue>(this IEnumerable<KeyValuePair<TKey, InTValue>> list) where InTValue : OutTValue
        {
            return list.ToDictionary(x => x.Key, x => (OutTValue)x.Value);
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }

        public static IList AddRange(this IList list, IEnumerable items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
            return list;
        }
        public static Dictionary<TKey, TItem> AddRange<TKey, TItem>(this Dictionary<TKey, TItem> collection, IEnumerable<KeyValuePair<TKey, TItem>> items)
        {
            foreach (var item in items)
            {
                collection.Add(item.Key, item.Value);
            }
            return collection;
        }

        /// <summary>
        /// Получить копию списка только для чтения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IReadOnlyList<T> Clone<T>(this IEnumerable<T> list)
        {
            return new List<T>(list).AsReadOnly();
        }

        /// <summary>
        /// "Клэмпит" правильно индекс от 0 от count листа. Через If конструкции для оптимизации
        /// <returns></returns>
        public static T GetCorrectItemFromIndex<T>(in IList<T> list, int queryIndex)
        {
            if (queryIndex < 0)
                return list[0];

            if (queryIndex >= list.Count)
            {
                int previous = list.Count - 1;

                if (previous < 0)
                    return list[0];

                return list[previous];
            }

            return list[queryIndex];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Dequeue<T>(this IList<T> list)
        {
            var buffer = list[0];
            list.RemoveAt(0);
            return buffer;
        }

        /// <summary>
        /// Создает копию списка другого типа
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<TOut> ToList<TIn, TOut>(this List<TIn> input) where TOut : TIn
        {
            var output = new List<TOut>();
            foreach (var item in input)
            {
                output.Add((TOut)item);
            }
            return output;
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }
}