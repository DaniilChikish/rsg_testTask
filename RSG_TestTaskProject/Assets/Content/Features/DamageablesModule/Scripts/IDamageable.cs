using System;
using Content.Features.InteractionModule;
using UnityEngine;

namespace Content.Features.DamageablesModule.Scripts
{
    public interface IDamageable
    {
        int ID { get; }
        float HealthPoint { get; }
        float MaxHealthPoint { get; }
        Vector3 Position { get; }
        DamageableType DamageableType { get; }
        bool IsActive { get; }
        AttackInteractable Interactable { get; }

        event Action OnHPChanged;
        event Action OnKilled;
        void Initialize(int ownerId, float maxHealth);
        void Damage(float damage);
        void RestoreHealth(float health);
        void SetHealth(float health);
    }
}