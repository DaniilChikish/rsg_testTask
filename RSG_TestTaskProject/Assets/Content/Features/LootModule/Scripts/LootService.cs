using Content.Features.StorageModule.Scripts;
using UnityEngine;
namespace Content.Features.LootModule.Scripts
{
    public class LootService : ILootService
    {
        private IItemFactory _itemFactory;

        public LootService(IItemFactory itemFactory) =>
            _itemFactory = itemFactory;

        public bool CollectLoot(Loot loot, IStorage storage)
        {
            var lootitems = _itemFactory.GetItems(loot.GetItemsInLoot());
            if (storage.IsEnoughSpace(lootitems))
            {
                storage.AddItems(lootitems);
                return true;
            }
            else
            {
                //TO DO: no storage space message
                Debug.Log("No storage space");
                return false;
            }
        }
    }
}