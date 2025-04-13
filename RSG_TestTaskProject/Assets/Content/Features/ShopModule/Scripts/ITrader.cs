using Content.Features.AIModule.Scripts.Entity;
using Content.Features.StorageModule.Scripts;

namespace Content.Features.ShopModule.Scripts
{
    public interface ITrader : IMonoBehaviour
    {
        void Trade(IPlayerStorage storage);
    }
}
