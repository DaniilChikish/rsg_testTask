using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

namespace Utility
{
    /// <summary>
    /// Класс контейнер для сериализации пар ключ-значение
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField, JsonProperty("keyValues")] private List<SerializedPair<TKey, TValue>> _keyValues = new List<SerializedPair<TKey, TValue>>();

        public TValue this[TKey key] { get => _keyValues.Find(x => x.Key.Equals(key)).Value; set => _keyValues.Find(x => x.Key.Equals(key)).Value = value; }

        [JsonIgnore]
        public ICollection<TKey> Keys => _keyValues.Select(x => x.Key).ToList();

        [JsonIgnore]
        public ICollection<TValue> Values => _keyValues.Select(x => x.Value).ToList();

        [JsonIgnore]
        public bool IsReadOnly => true;

        public SerializableDictionary()
        {
        }
        /// <summary>
        /// Get converted System.Dictionary
        /// </summary>
        public Dictionary<TKey, TValue> GetDictionary()
        {
            var dict = new Dictionary<TKey, TValue>(_keyValues.Count);
            foreach (var item in _keyValues)
                dict.Add(item.Key, item.Value);
            return dict;
        }
        public List<KeyValuePair<TKey, TValue>> GetList()
        {
            var output = new List<KeyValuePair<TKey, TValue>>(_keyValues.Count);
            foreach (var item in _keyValues)
                output.Add(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
            return output;
        }

        public void Add(TKey key, TValue value)
        {
            _keyValues.Add(new SerializedPair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _keyValues.Add(new SerializedPair<TKey, TValue>(item.Key, item.Value));
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _keyValues.Exists(x => x.Key.Equals(item.Key) && x.Value.Equals(item.Value));
        }

        public bool ContainsKey(TKey key)
        {
            return _keyValues.Exists(x => x.Key.Equals(key));
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (this.ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
        public SerializedPair<TKey, TValue> GetAt(int index)
        {
            return _keyValues[index];
        }
        public List<SerializedPair<TKey, TValue>>.Enumerator GetEnumerator()
        {
            return _keyValues.GetEnumerator();
        }

        public void Clear()
        {
            _keyValues.Clear();
        }
    }

    [Serializable]
    public class SerializedPair<TKey, TValue>
    {
        [SerializeField] public TKey Key;
        [SerializeField] public TValue Value;
        public SerializedPair()
        { }
        public SerializedPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
