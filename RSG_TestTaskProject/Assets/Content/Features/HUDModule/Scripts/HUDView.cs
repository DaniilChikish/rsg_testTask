using Content.Features.StorageModule.Scripts;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Content.Features.HUDModule
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private SliderBarUIElement hpBar;
        [SerializeField] private Transform _inventoryContainer;
        [SerializeField] private GameObject _inventoryCellPrefab;
        [SerializeField] private TextMeshProUGUI _goldCount;
        private HUDController _controller;
        private Dictionary<string, ItemCellElement> _instants = new();
        private DiContainer _diContainer;

        [Inject]
        public void InjectDependencies(HUDController controller, DiContainer diContainer)
        {
            _controller = controller;
            _controller.Register(this);
            _diContainer = diContainer;
        }

        private void Start()
        {

        }

        public void SetPlayerHp(float value, float maxValue)
        {
            hpBar.FillAmount = value / maxValue;
        }

        public void UpdateInventory(IEnumerable<KeyValuePair<Item, int>> storageItems)
        {
            var deprecatedItems = _instants.Where(x => !storageItems.Any(y => x.Key == y.Key.Name)).Select(x => x.Value).ToList();
            foreach (var item in _instants)
            {
                Destroy(item.Value.gameObject);
            }
            _instants.Clear();
            foreach (var item in storageItems)
            {
                var newItem = _diContainer.InstantiatePrefabForComponent<ItemCellElement>(_inventoryCellPrefab, _inventoryContainer);
                newItem.Setup(item.Key.Icon, item.Value, () => _controller.OnItemClick(item.Key));
                _instants.Add(item.Key.Name, newItem);
            }
        }
        public void UpdateGoldCount(int  count) { _goldCount.text = count.ToString(); }
    }
}