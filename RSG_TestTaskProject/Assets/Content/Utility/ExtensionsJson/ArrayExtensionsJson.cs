using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Utility.Extensions.Json
{
    public static class ArrayExtensionsJson
    {
        /// <summary>
        /// For work with document data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool SmartTryGetValue<T>(this Dictionary<string, object> data, string key, out T value)
        {
            if (data.TryGetValue(key, out var obj))
            {
                if (obj is JObject jObject)
                {
                    value = jObject.ToObject<T>();
                    return true;
                }

                if (obj is JArray jArray)
                {
                    value = jArray.ToObject<T>();
                    return true;
                }

                if (obj is string json)
                {
                    value = JsonConvert.DeserializeObject<T>(json);
                    return true;
                }

                if (obj == null)
                {
                    value = default;
                    return false;
                }

                if (data.TryGetValue(key, out value)) return true;
                Debug.LogError($"Cant read data. type: {data[key]?.GetType()}");
                return false;
            }

            value = default;
            return false;
        }
        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out Vector2 value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                if (obj is JArray jArray)
                {
                    var arr = jArray.ToObject<float[]>();
                    if (arr.Length == 2)
                    {
                        value = new Vector2(arr[0], arr[1]);
                        return true;
                    }
                }
            }
            value = default;
            return false;
        }
        public static bool TryGetValue(this IDictionary<string, object> dictionary, string key, out Vector3 value)
        {
            if (dictionary.TryGetValue(key, out var obj))
            {
                if (obj is JArray jArray)
                {
                    var arr = jArray.ToObject<float[]>();
                    if (arr.Length == 3)
                    {
                        value = new Vector3(arr[0], arr[1], arr[2]);
                        return true;
                    }
                }
            }
            value = default;
            return false;
        }
        public static T GetJsonValue<T>(this IDictionary<string, object> data, string key)
        {
            if (data == null)
            {
                Debug.LogError($"Data is null.");
                return default;
            }

            if (!data.TryGetValue(key, out var obj)) return default;

            if (obj is JObject jObject)
            {
                return jObject.ToObject<T>();
            }

            if (obj is JArray jArray)
            {
                return jArray.ToObject<T>();
            }

            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), obj.ToString());
            }

            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T ToObject<T>(this object data)
        {
            if (data == null)
            {
                Debug.LogError($"Data is null.");
                return default;
            }

            if (data is JObject jObject)
            {
                return jObject.ToObject<T>();
            }

            if (data is JArray jArray)
            {
                return jArray.ToObject<T>();
            }

            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), data.ToString());
            }

            return (T)Convert.ChangeType(data, typeof(T));
        }
    }
}
