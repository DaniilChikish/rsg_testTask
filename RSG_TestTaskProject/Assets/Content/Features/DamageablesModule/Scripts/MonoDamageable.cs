using System;
using Content.Features.AIModule.Scripts;
using Content.Features.InteractionModule;
using UnityEngine;

namespace Content.Features.DamageablesModule.Scripts {
    public class MonoDamageable : MonoBehaviour, IDamageable {
        private int _ownerId;
        [SerializeField] private float _health;
        [SerializeField] private float _maxhealth;
        [SerializeField] private DamageableType _damageableType;
        [SerializeField] private AttackInteractable _attackInteractable;
    
        public Vector3 Position =>
            transform.position;
        public DamageableType DamageableType =>
            _damageableType;
        public bool IsActive =>
            _health > 0;
        public AttackInteractable Interactable =>
            _attackInteractable;
        public int ID => _ownerId;
        public float HealthPoint => _health;

        public float MaxHealthPoint => _maxhealth;

        public event Action OnHPChanged;
        public event Action OnKilled;

        public void Initialize(int ownerId, float maxHealth)
        {
            _ownerId = ownerId;
            _health = maxHealth;
            _maxhealth = maxHealth;
        }

        public void Damage(float damage) {
            _health = Mathf.Clamp(_health - damage, 0, _maxhealth);
            OnHPChanged?.Invoke();

            if (_health > 0)
                return;

            OnKilled?.Invoke();
            Destroy(gameObject);
        }
        public void RestoreHealth(float health)
        {
            _health = Mathf.Clamp(_health + health, 0, _maxhealth);
            OnHPChanged?.Invoke();
        }

        public void SetHealth(float health) =>
            _health = health;
    }
}