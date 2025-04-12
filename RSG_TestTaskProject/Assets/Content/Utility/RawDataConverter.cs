using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public static class RawDataConverter
    {
        /// <summary>
        /// From "1:abc,2:bcd"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, int>> ToStringIntPairs(string rawData, char delimiter = ',')
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }

            var output = new List<KeyValuePair<string, int>>();
            var rows = rawData.Split(delimiter);
            string[] pair;
            foreach (var row in rows)
            {
                pair = row.Split(':');
                if (int.TryParse(pair[0], out int count))
                {
                    output.Add(new KeyValuePair<string, int>(pair[1], count));
                }
                else
                {
                    Debug.LogError("Can't parse item " + row);
                }
            }
            return output;
        }

        public static Vector2Int ToVector2(IList<int> rawData)
        {
            if (rawData.Count < 2)
                throw new System.Exception("Input array too short for vector.");
            return new Vector2Int(rawData[0], rawData[1]);
        }
        public static Vector3Int ToVector3(IList<int> rawData)
        {
            if (rawData.Count < 3)
                throw new System.Exception("Input array too short for vector."); 
            return new Vector3Int(rawData[0], rawData[1], rawData[2]);
        }
        public static Vector2 ToVector2(IList<float> rawData)
        {
            if (rawData.Count < 2)
                throw new System.Exception("Input array too short for vector.");
            return new Vector2(rawData[0], rawData[1]);
        }
        public static Vector3 ToVector3(IList<float> rawData)
        {
            if (rawData.Count < 3)
                throw new System.Exception("Input array too short for vector.");
            return new Vector3(rawData[0], rawData[1], rawData[2]);
        }

        /// <summary>
        /// From "1.5:abc,2.99:bcd"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, float>> ToStringFloatPairs(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }

            var output = new List<KeyValuePair<string, float>>();
            var rows = rawData.Split(',');
            string[] pair;
            foreach (var row in rows)
            {
                pair = row.Split(':');
                if (float.TryParse(pair[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float count))
                {
                    output.Add(new KeyValuePair<string, float>(pair[1], count));
                }
                else
                {
                    Debug.LogError("Can't parse item " + row);
                }
            }
            return output;
        }

        /// <summary>
        /// From "1:2,3:4"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<KeyValuePair<int, int>> ToIntIntPairs(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }

            var output = new List<KeyValuePair<int, int>>();
            var rows = rawData.Split(',');
            string[] pair;
            foreach (var row in rows)
            {
                pair = row.Split(':');
                if (int.TryParse(pair[0], out int A) && int.TryParse(pair[1], out int B))
                {
                    output.Add(new KeyValuePair<int, int>(A, B));
                }
                else
                {
                    Debug.LogError("Can't parse item " + row);
                }
            }
            return output;
        }

        /// <summary>
        /// From "1:0.99,2:1.99"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static Dictionary<int, float> ToIntFloatDictionary(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }

            var output = new Dictionary<int, float>();
            var rows = rawData.Split(',');
            string[] pair;
            foreach (var row in rows)
            {
                pair = row.Split(':');

                if (pair.Length == 2 &&
                    int.TryParse(pair[0], out int A) &&
                    float.TryParse(pair[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float B))
                {
                    if (!output.ContainsKey(A))
                    {
                        output.Add(A, B);
                    }
                    else
                    {
                        Debug.LogError($"Duplicate key of item {row} in {rawData}");
                    }
                }
                else
                {
                    Debug.LogError($"Can't parse item {row} in {rawData}");
                }
            }
            return output;
        }

        /// <summary>
        /// From "1,2,3,4"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<int> ToIntList(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }
            var output = new List<int>();
            var rows = rawData.Split(',');
            foreach (var row in rows)
            {
                if (int.TryParse(row, out int A))
                    output.Add(A);
            }
            return output;
        }

        /// <summary>
        /// From "1,1.5,2,2.5,"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<float> ToFloatList(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }
            var output = new List<float>();
            var rows = rawData.Split(',');
            foreach (var row in rows)
            {
                if (float.TryParse(row, NumberStyles.Any, CultureInfo.InvariantCulture, out float B))
                    output.Add(B);
            }
            return output;
        }

        /// <summary>
        /// From "1-2"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static Vector2Int? ToIntIntVector(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }
            var pair = rawData.Split('-');

            if (pair.Length > 0 && int.TryParse(pair[0], out int A))
            {
                if (pair.Length > 1 && int.TryParse(pair[1], out int B))
                    return new Vector2Int(A, B);
                else 
                    return new Vector2Int(A, A);
            }
            else
            {
                Debug.LogError($"Can't parse item row {rawData}");
                return null;
            }
        }

        /// <summary>
        /// From "itemA/itemB"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<string> ToORListString(string rawData)
        {
            return rawData.Split('/').ToList();
        }

        /// <summary>
        /// From "itemA,itemB"
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static List<string> ToANDListString(string rawData)
        {
            return rawData.Split(',').ToList();
        }

        /// <summary>
        /// Normalize input to values betwen 0-1
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<float> Normalize(List<int> list)
        {
            var sum = list.Sum();
            return list.Select(x => x / (float)sum).ToList();
        }

        /// <summary>
        /// Normalize input to values betwen 0-1
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<float> Normalize(List<float> list)
        {
            var sum = list.Sum();
            return list.Select(x => x / sum).ToList();
        }
    }
}