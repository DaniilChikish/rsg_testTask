namespace Content.Features.StorageModule.Scripts {
    public class StorageFactory : IStorageFactory {
        private readonly StorageConfiguration _storageConfiguration;
        public StorageFactory(StorageConfiguration storageConfiguration) =>
            _storageConfiguration = storageConfiguration;
        public IStorage GetStorage() =>
            new StandardStorage(_storageConfiguration);
    }
}