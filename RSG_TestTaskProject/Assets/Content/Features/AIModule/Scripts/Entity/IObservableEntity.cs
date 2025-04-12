using Content.Features.DamageablesModule.Scripts;
using Content.Features.StorageModule.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Features.AIModule.Scripts.Entity
{
    public interface IObservableEntity : IEntity
    {
        EntityData EntityData { get; }
        IDamageable Damageable { get; }
        IStorage Storage { get; }
    }
}
