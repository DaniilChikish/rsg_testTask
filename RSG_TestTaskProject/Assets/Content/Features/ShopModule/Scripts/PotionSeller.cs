using Content.Features.LootModule.Scripts;
using Content.Features.StorageModule.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Content.Features.ShopModule.Scripts
{
    public class PotionSeller : BaseTrader
    {
        private IItemFactory _itemFactory;

        [Inject]
        public void Inject(IItemFactory itemFactory) =>
            _itemFactory = itemFactory;

        private int SellItemsFromStorage(IEnumerable<Item> items, IStorage storage)
        {
            storage.RemoveItems(items);

            int sumOfMoney = 0;
            foreach (int price in items.Select(item => item.Price))
                sumOfMoney += price;

            return sumOfMoney;
        }

        public override void Trade(IPlayerStorage storage)
        {
            if (storage.IsEnoughGold(10))
            {
                var newItem = _itemFactory.GetItem(ItemType.Potion);
                if (storage.IsEnoughSpace(newItem))
                {
                    storage.AddItem(newItem);
                    storage.RemoveGold(10);
                }
                else
                {
                    Debug.Log("No space");
                }
            }
            else
            {
                Debug.Log("No Gold");
            }
        }
    }
}