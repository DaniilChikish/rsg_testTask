using System.Collections.Generic;

namespace Content.Features.StorageModule.Scripts {
    public class ItemFactory : IItemFactory {
        private readonly ItemsConfiguration _itemsConfiguration;

        public ItemFactory(ItemsConfiguration itemsConfiguration) =>
            _itemsConfiguration = itemsConfiguration;

        public Item GetItem(ItemType itemType) =>
            new (_itemsConfiguration.GetItemConfiguration(itemType));

        public IEnumerable<Item> GetItems(IEnumerable<ItemType> itemTypes)
        {
            var items = new List<Item>();
            foreach (var itemType in itemTypes)
            {
                items.Add(GetItem(itemType));
            }
            return items;
        }
    }
}