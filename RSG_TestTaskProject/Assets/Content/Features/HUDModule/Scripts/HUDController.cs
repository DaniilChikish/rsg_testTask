using Content.Features.AIModule.Scripts.Entity;
using Content.Features.StorageModule.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content.Features.HUDModule
{
    public class HUDController
    {
        private HUDView _hudView;
        private PlayerEntityModel _playerEntityModel;

        public HUDController(PlayerEntityModel playerEntityModel)
        {
            _playerEntityModel = playerEntityModel;
            _playerEntityModel.OnPlayerEntityChanged += OnPlayerEntityChanged;
        }

        private void OnPlayerEntityChanged()
        {
            SetupPlayerView();
        }

        private void SetupPlayerView()
        {
            _playerEntityModel.PlayerEntity.Storage.OnItemAdded += OnAddItem;
            _playerEntityModel.PlayerEntity.Storage.OnItemRemoved += OnRemoveItem;
            _hudView.UpdateInventory(DestinctInventory(_playerEntityModel.PlayerEntity.Storage.GetAllItems()));
        }

        private IEnumerable<KeyValuePair<Item, int>> DestinctInventory(List<Item> items)
        {
            var distinctrow = new Dictionary<Item, int>();
            foreach (Item item in items)
            {
                var existed = distinctrow.FirstOrDefault(x => x.Key.Name == item.Name);
                if (existed.Key == default)
                {
                    distinctrow.Add(item, 1);
                }
                else
                {
                    distinctrow[existed.Key]++;
                }
            }
            return distinctrow;
        } 

        private void OnAddItem(Item item)
        {
            _hudView.UpdateInventory(DestinctInventory(_playerEntityModel.PlayerEntity.Storage.GetAllItems()));
        }
        private void OnRemoveItem(Item item)
        {
            _hudView.UpdateInventory(DestinctInventory(_playerEntityModel.PlayerEntity.Storage.GetAllItems()));
        }
        public void Register(HUDView hudView)
        {
            _hudView = hudView;
        }

        internal void OnItemClick(Item item)
        {
            Debug.Log("Click on " + item.Name);
        }
    }
}