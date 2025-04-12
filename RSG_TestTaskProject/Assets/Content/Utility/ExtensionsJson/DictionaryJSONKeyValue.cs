using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DictionaryArray<TKey, TValue> : List<SerializedPair<TKey, TValue>>
    {
        public TValue this[TKey key] { get => this.Find(x => x.Key.Equals(key)).Value; set => this.Find(x => x.Key.Equals(key)).Value = value; }

        [JsonIgnore]
        public ICollection<TKey> Keys => this.Select(x => x.Key).ToList();

        [JsonIgnore]
        public ICollection<TValue> Values => this.Select(x => x.Value).ToList();

        [JsonIgnore]
        public bool IsReadOnly => true;

        public DictionaryArray()
        {
        }
        /// <summary>
        /// Get converted System.Dictionary
        /// </summary>
        public Dictionary<TKey, TValue> GetDictionary()
        {
            var dict = new Dictionary<TKey, TValue>(this.Count);
            foreach (var item in this)
                dict.Add(item.Key, item.Value);
            return dict;
        }
        public List<KeyValuePair<TKey, TValue>> GetList()
        {
            var output = new List<KeyValuePair<TKey, TValue>>(this.Count);
            foreach (var item in this)
                output.Add(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
            return output;
        }

        public void Add(TKey key, TValue value)
        {
            this.Add(new SerializedPair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(new SerializedPair<TKey, TValue>(item.Key, item.Value));
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.Exists(x => x.Key.Equals(item.Key) && x.Value.Equals(item.Value));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Exists(x => x.Key.Equals(key));
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
        public List<SerializedPair<TKey, TValue>>.Enumerator GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Clear()
        {
            this.Clear();
        }
    }
}
