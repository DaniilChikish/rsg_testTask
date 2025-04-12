using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility.Extensions
{
    public static class ArrayExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int size)
        {
            var i = 0;
            return
                from element in source
                group element by i++ / size into splitGroups
                select splitGroups.AsEnumerable();
        }

        public static IEnumerable<T> Reversed<T>(this IEnumerable<T> list)
        {
            return list.Reverse();
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return (T)enumerator.Current;
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }


        public static void ForEach<T>(this IEnumerable<T> first, Action<T> action)
        {
            foreach (T t in first)
            {
                action(t);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> first, Action<T, int> action)
        {
            var index = 0;
            foreach (T t in first)
            {
                action(t, index++);
            }
        }


        public static T TryGet<T>(this List<T> list, int i)
        {
            if (i < 0 || list.Count <= i)
                return default;
            return list[i];
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            var n = list.Count;
            var newList = list.CopyList();
            while (n > 0)
            {
                var k = Random.Range(0, n);
                var item = newList[n - 1];
                newList[n - 1] = newList[k];
                newList[k] = item;
                n--;
            }
            return newList;
        }

        public static T[] Shuffle<T>(this T[] array)
        {
            var n = array.Length;
            var newArray = array.CopyArray();
            while (n > 0)
            {
                var k = Random.Range(0, n);
                var item = newArray[n - 1];
                newArray[n - 1] = newArray[k];
                newArray[k] = item;
                n--;
            }
            return newArray;
        }

        public static List<T> CopyList<T>(this List<T> list)
        {
            if (list == null)
                return new List<T>();
            var n = list.Count;
            var newList = new List<T>();
            for (var i = 0; i < n; i++)
            {
                newList.Add(list[i]);
            }
            return newList;
        }

        public static T[] CopyArray<T>(this T[] array)
        {
            var n = array.Length;
            var newArray = new T[n];
            for (var i = 0; i < n; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T GetRandomElement<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out int value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                value = Convert.ToInt32(obj);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out long value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                value = Convert.ToInt64(obj);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out float value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                value = Convert.ToSingle(obj);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out double value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                value = Convert.ToDouble(obj);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out string value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                value = obj as string;
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out bool value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                value = Convert.ToBoolean(obj);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                if (obj is long)
                    obj = Convert.ToInt32(obj);

                if (obj is T obj1)
                {
                    value = obj1;
                    return true;
                }
                Debug.LogError($"Type mismatch! Key: {key}");
            }
            value = default;
            return false;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return true;
            }

            return source.Any() == false;
        }
    }
}