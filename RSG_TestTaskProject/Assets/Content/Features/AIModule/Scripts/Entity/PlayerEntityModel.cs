using System;

namespace Content.Features.AIModule.Scripts.Entity {
    public class PlayerEntityModel {
        private IObservableEntity _playerEntity;

        public IObservableEntity PlayerEntity {
            get =>
                _playerEntity;
            set {
                if(_playerEntity == value)
                    return;
                
                _playerEntity = value;
                OnPlayerEntityChanged?.Invoke();
            }
        }

        public event Action OnPlayerEntityChanged;
    }
}