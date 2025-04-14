using System;
using System.Collections.Generic;

namespace Content.Features.StorageModule.Scripts
{
    public class PlayerEntityStorage : BaseEntityStorage, IPlayerStorage
    {
        private PlayerStorageModel _storageModel;

        public event Action OnGoldAdded;
        public event Action OnGoldRemoved;

        protected override IList<Item> Items => _storageModel.Items;

        public int Gold => _storageModel.GoldCount;

        public PlayerEntityStorage(StorageConfiguration storageConfiguration, PlayerStorageModel storageModel) : base(storageConfiguration)
        {
            _storageModel = storageModel;
            TryRecalculateSpace();
        }
        public bool IsEnoughGold(int price)
        {
           return Gold >= price;
        }
        public void AddGold(int count)
        {
            _storageModel.GoldCount += count;
            OnGoldAdded?.Invoke();
        }

        public void RemoveGold(int count)
        {
            _storageModel.GoldCount -= count;
            OnGoldRemoved?.Invoke();
        }
    }
}