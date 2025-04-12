using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Features.StorageModule.Scripts
{
    public interface IPlayerStorage : IStorage
    {
        public int Gold { get; }
        public event Action OnGoldAdded;
        public event Action OnGoldRemoved;
        bool IsEnoughGold(int price);
        void AddGold(int count);
        void RemoveGold(int count);
    }
}
