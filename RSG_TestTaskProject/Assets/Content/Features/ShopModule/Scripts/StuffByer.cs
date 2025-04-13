using Content.Features.StorageModule.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content.Features.ShopModule.Scripts
{
    public class StuffByer : BaseTrader
    {
        private int SellAllItemsFromStorage(IStorage storage)
        {
            int sumOfMoney = 0;

            foreach (int price in storage.GetAllItems().Select(item => item.Price))
                sumOfMoney += price;

            storage.RemoveAllItems();
            Debug.LogError("Recieved " + sumOfMoney);
            return sumOfMoney;
        }

        private int SellItemFromStorage(Item item, IStorage storage)
        {
            storage.RemoveItem(item);

            return item.Price;
        }

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
            var forSale = storage.GetAllItems().Where(x => x.Name != "Potion");
            storage.AddGold(SellItemsFromStorage(forSale, storage));
        }
    }
}
