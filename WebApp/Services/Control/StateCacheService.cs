using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.Services.Control
{
    public class StateCacheService : IStateCacheService
    {
        Dictionary<Guid, object> _stateCache = new Dictionary<Guid, object>();

        public void Set(Guid id, object payload)
        {
            _stateCache[id] = payload;
        }
    }
}
