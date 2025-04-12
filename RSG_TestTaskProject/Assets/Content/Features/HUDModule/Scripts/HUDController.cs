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
        private IPlayerStorage _playerStorage;

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
            _playerStorage = _playerEntityModel.PlayerEntity.Storage as IPlayerStorage;
            
            _playerEntityModel.PlayerEntity.Damageable.OnHPChanged += OnHPChanged;

            _playerStorage.OnItemAdded += OnAddItem;
            _playerStorage.OnItemRemoved += OnRemoveItem;
            _hudView.UpdateInventory(DestinctInventory(_playerEntityModel.PlayerEntity.Storage.GetAllItems()));

            _playerStorage.OnGoldAdded += OnAddGold;
            _playerStorage.OnGoldRemoved += OnRemoveGold;

            _hudView.UpdateGoldCount(_playerStorage.Gold);
        }

        private void OnHPChanged()
        {
            _hudView.SetPlayerHp(_playerEntityModel.PlayerEntity.Damageable.HealthPoint, _playerEntityModel.PlayerEntity.Damageable.MaxHealthPoint);
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
            if (item.Name == "Potion")
            {
                _playerEntityModel.PlayerEntity.Damageable.RestoreHealth(20);
                _playerEntityModel.PlayerEntity.Storage.RemoveItem(item);
            }
        }

        private void OnAddGold()
        {
            _hudView.UpdateGoldCount(_playerStorage.Gold);
        }

        private void OnRemoveGold()
        {
            _hudView.UpdateGoldCount(_playerStorage.Gold);
        }
    }
}