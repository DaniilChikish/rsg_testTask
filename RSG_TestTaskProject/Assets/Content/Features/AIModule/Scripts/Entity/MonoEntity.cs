using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.StorageModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity {
    public class MonoEntity : MonoBehaviour, IObservableEntity
    {
        [SerializeField] private EntityContext _entityContext;
        [SerializeField] private EntityType _entityType;
        [SerializeField] private bool _isAggressive;
        
        private IEntityBehaviour _currentBehaviour;
        private IEntityDataService _entityDataService;
        private IStorageFactory _storageFactory;
        private IEntityBehaviourFactory _entityBehaviourFactory;

        public EntityData EntityData => _entityContext.EntityData;

        public IDamageable Damageable => _entityContext.EntityDamageable;

        public IStorage Storage => _entityContext.Storage;

        [Inject]
        public void InjectDependencies(IEntityDataService entityDataService, IStorageFactory storageFactory, IEntityBehaviourFactory entityBehaviourFactory) {
            _entityBehaviourFactory = entityBehaviourFactory;
            _storageFactory = storageFactory;
            _entityDataService = entityDataService;
        }
        private void OnEnable()
        {
            _entityContext.Entity = this;
            _entityContext.EntityDamageable = GetComponent<IDamageable>();
            _entityContext.EntityData = _entityDataService.GetEntityData(_entityType);
            _entityContext.EntityDamageable.SetHealth(_entityContext.EntityData.StartHealth);
            _entityContext.Storage = _storageFactory.GetStorage(_entityType);
        }
        private void Start() 
        {       
            SetDefaultBehaviour();
        }

        private void Update() =>
            _currentBehaviour.Process();

        private void OnDestroy() {
            if (_currentBehaviour == null)
                return;

            _currentBehaviour.Stop();
            _currentBehaviour.OnBehaviorEnd -= OnBehaviourEnded;
        }

        public void SetBehaviour(IEntityBehaviour entityBehaviour) {
            if(_currentBehaviour != null) {
                _currentBehaviour.Stop();
                _currentBehaviour.OnBehaviorEnd -= OnBehaviourEnded;
            }
            _currentBehaviour = entityBehaviour;
            _currentBehaviour.OnBehaviorEnd += OnBehaviourEnded;
            _currentBehaviour.InitContext(_entityContext);
            _currentBehaviour.Start();
        }

        private void OnBehaviourEnded() =>
            SetDefaultBehaviour();

        private void SetDefaultBehaviour() {
            if (_isAggressive)
                SetBehaviour(_entityBehaviourFactory.GetEntityBehaviour<IdleSearchForTargetsEntityBehaviour>());
            else
                SetBehaviour(_entityBehaviourFactory.GetEntityBehaviour<IdleEntityBehaviour>());
        }
    }
}