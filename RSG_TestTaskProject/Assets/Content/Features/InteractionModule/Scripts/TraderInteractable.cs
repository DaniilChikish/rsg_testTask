using Content.Features.AIModule.Scripts.Entity;
using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.ShopModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.InteractionModule
{
    public class TraderInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private BaseTrader trader;
        private IEntityBehaviourFactory _entityBehaviourFactory;
        
        [Inject]
        public void InjectDependencies(IEntityBehaviourFactory entityBehaviourFactory) =>
            _entityBehaviourFactory = entityBehaviourFactory;

        public void Interact(IEntity entity) {
            var sellItemsEntityBehaviour = _entityBehaviourFactory.GetEntityBehaviour<SellItemsEntityBehaviour>();
            sellItemsEntityBehaviour.SetTrader(trader);
            entity.SetBehaviour(sellItemsEntityBehaviour);
        }
    }
}