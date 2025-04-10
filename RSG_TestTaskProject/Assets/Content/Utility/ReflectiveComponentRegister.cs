using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public abstract class ReflectiveComponentRegister
    {
        private readonly Dictionary<string, ComponentRegisterEntry> _behavioursEntries;
        protected ComponentRegisterEntry defaultValue;
        public ReflectiveComponentRegister()
        {
            _behavioursEntries = new Dictionary<string, ComponentRegisterEntry>();
            Setup();
        }
        protected abstract void Setup();

        protected void Add(Type typeEntry)
        {
            _behavioursEntries.Add(typeEntry.Name, new ComponentRegisterEntry(typeEntry));
        }

        /// <summary>
        /// возвращаем объект класса, хранящий тип поведения
        /// </summary>
        /// <param name="key">ключ типа команды</param>
        /// <returns>объект класса, хранящий тип команды <see cref="ComponentRegisterEntry"/></returns>
        public ComponentRegisterEntry GetEntry(string key)
        {
            _behavioursEntries.TryGetValue(key, out var entry);

            if (entry == null)
            {
                if (defaultValue != null)
                {
                    Debug.LogWarning($"Type-entry {key} not found. Return default {this}");
                    return defaultValue;
                }
                else
                {
                    Debug.LogException(new Exception($"Type-entry {key} not found. {this}"));
                }
            }
            return entry;
        }
    }
}
