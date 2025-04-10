using System.Collections;
using System.Collections.Generic;

namespace Content.Features.StorageModule.Scripts {
    public interface IItemFactory {
        public Item GetItem(ItemType itemType);
        public IEnumerable<Item> GetItems(IEnumerable<ItemType> itemTypes);
    }
}