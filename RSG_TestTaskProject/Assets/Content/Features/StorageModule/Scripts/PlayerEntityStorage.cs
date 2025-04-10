using System;
using System.Collections.Generic;
using System.Linq;

namespace Content.Features.StorageModule.Scripts
{
    public class PlayerEntityStorage : BaseEntityStorage
    {
        private PlayerStorageModel _storageModel;

        protected override IList<Item> Items => _storageModel.Items;
        public PlayerEntityStorage(StorageConfiguration storageConfiguration, PlayerStorageModel storageModel) : base(storageConfiguration)
        {
            _storageModel = storageModel;
            TryRecalculateSpace();
        }
    }
}