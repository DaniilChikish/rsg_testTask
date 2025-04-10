using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(menuName = "Configurations/Inventory/" + nameof(StorageConfiguration),
        fileName = nameof(StorageConfiguration) + "_Default", order = 0)]
    public class StorageConfiguration : ScriptableObject
    {
        [Header("Storage")]
        [field: SerializeField] public int StorageCapacity { get; private set; } = 10;
    }
}
