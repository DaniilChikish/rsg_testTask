using Content.Features.AIModule.Scripts.Entity;

namespace Content.Features.StorageModule.Scripts
{
    public class StorageFactory : IStorageFactory
    {
        private readonly StorageConfiguration _storageConfiguration;
        private readonly PlayerStorageModel _playerStorageModel;

        public StorageFactory(StorageConfiguration storageConfiguration, PlayerStorageModel playerStorageModel)
        {
            _storageConfiguration = storageConfiguration;
            _playerStorageModel = playerStorageModel;
        }
        public IStorage GetStorage(EntityType type)
        {
            switch (type)
            {
                case EntityType.Player:
                    {
                        return new PlayerEntityStorage(_storageConfiguration, _playerStorageModel);
                    }
                case EntityType.Seller:
                default:
                    {
                        return new StandardStorage(_storageConfiguration);
                    }
            }
        }
    }
}