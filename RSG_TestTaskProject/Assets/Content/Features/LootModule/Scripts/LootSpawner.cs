using UnityEngine;
using Utility;
using Utility.Randomizer;
using Zenject;

namespace Content.Features.LootModule.Scripts
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private int _tries;
        [SerializeField] private SerializableDictionary<Loot, float> _lootToSpawn;
        private DiContainer _diContainer;

        [Inject]
        public void InjectDependencies(DiContainer diContainer) =>
            _diContainer = diContainer;

        public void SpawnLoot()
        {
            for (int i = 0; i < _tries; i++) 
            {
                var loot = UnevenDice.Throw(_lootToSpawn.GetDictionary()).Key;
                _diContainer.InstantiatePrefab(loot.gameObject, transform.position, Quaternion.identity, null);
            }
        }
    }
}