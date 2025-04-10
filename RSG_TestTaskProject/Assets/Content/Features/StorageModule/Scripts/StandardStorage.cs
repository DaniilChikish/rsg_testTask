using System;
using System.Collections.Generic;
using System.Linq;

namespace Content.Features.StorageModule.Scripts {
    public class StandardStorage : IStorage {
        private List<Item> _items = new List<Item>();
        private StorageConfiguration _storageConfiguration;
        private bool _dirty;
        private float _allocatedSpace;

        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;

        public StandardStorage(StorageConfiguration storageConfiguration)
        {
            _storageConfiguration = storageConfiguration;
        }

        public List<Item> GetAllItems() =>
            _items.ToList();

        public bool IsEnoughSpace(Item item)
        {
            TryRecalculateSpace();
            return (_allocatedSpace + item.Weight <= _storageConfiguration.StorageCapacity);
        }
        public bool IsEnoughSpace(IEnumerable<Item> items)
        {
            TryRecalculateSpace();
            return (_allocatedSpace + CalculateWeight(items) <= _storageConfiguration.StorageCapacity);
        }

        private void TryRecalculateSpace()
        {
            if (_dirty)
            {
                _dirty = false;
                _allocatedSpace = CalculateWeight(_items);
            }
        }

        private float CalculateWeight(IEnumerable<Item> items)
        {
            var weight = 0f;
            foreach (Item item in items)
                weight += item.Weight;
            return weight;
        }

        public void AddItem(Item item) {
            _dirty = true;
            if(_items.Contains(item))
                return;
        
            _items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        public void AddItems(IEnumerable<Item> items) {
            _dirty = true;
            foreach (Item item in items)
                AddItem(item);
        }

        public void RemoveItem(Item item) {
            _dirty = true;
            if (_items.Contains(item) is false)
                return;

            _items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        public void RemoveItems(List<Item> items) {
            _dirty = true;
            foreach (Item item in items)
                RemoveItem(item);
        }

        public void RemoveAllItems() {
            _dirty = true;
            foreach (Item item in _items)
                RemoveItem(item);
        }
    }
}