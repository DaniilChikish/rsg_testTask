using Content.Features.StorageModule.Scripts;
using System;
using UnityEngine;

namespace Content.Features.ShopModule.Scripts
{
    public abstract class BaseTrader : MonoBehaviour, ITrader
    {
        public abstract void Trade(IPlayerStorage storage);
    }
}
