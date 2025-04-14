using System;
using System.Collections.Generic;
using System.Linq;

namespace Content.Features.StorageModule.Scripts {
    public class StandardStorage : BaseEntityStorage
    {
        private List<Item> _items = new List<Item>();
        protected override IList<Item> Items => _items;
        public StandardStorage(StorageConfiguration storageConfiguration): base(storageConfiguration)
        {
        }
    }
}