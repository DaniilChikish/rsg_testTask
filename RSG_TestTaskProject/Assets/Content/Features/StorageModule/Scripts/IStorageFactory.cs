using Content.Features.AIModule.Scripts.Entity;

namespace Content.Features.StorageModule.Scripts {
    public interface IStorageFactory {
        public IStorage GetStorage(EntityType _entityType);
    }
}