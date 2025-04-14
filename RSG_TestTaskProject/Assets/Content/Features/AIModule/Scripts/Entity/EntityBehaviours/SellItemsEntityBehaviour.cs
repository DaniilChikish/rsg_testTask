using System;
using System.Linq;
using Content.Features.ShopModule.Scripts;
using Content.Features.StorageModule.Scripts;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours {
    public class SellItemsEntityBehaviour : IEntityBehaviour
    {
        private EntityContext _entityContext;
        private ITrader _trader;
        
        public event Action OnBehaviorEnd;
        public void InitContext(EntityContext entityContext) =>
            _entityContext = entityContext;
        
        public void SetTrader(ITrader trader) =>
            _trader = trader;

        public void Start() =>
            _entityContext.NavMeshAgent.speed = _entityContext.EntityData.Speed;

        public void Process() {
            if(IsNearTheTarget())
                SellItems();
            else
                MoveToTarget();
        }

        public void Stop() { }

        private void MoveToTarget() =>
            _entityContext.NavMeshAgent.SetDestination(_trader.transform.position);

        private void StopMoving() =>
            _entityContext.NavMeshAgent.ResetPath();

        private bool IsNearTheTarget() =>
            Vector3.Distance(_entityContext.EntityDamageable.Position, _trader.transform.position) <= _entityContext.EntityData.InteractDistance;

        private void SellItems() {

            _trader.Trade(_entityContext.Storage as IPlayerStorage);
            StopMoving();
            OnBehaviorEnd?.Invoke();
        }
    }
}