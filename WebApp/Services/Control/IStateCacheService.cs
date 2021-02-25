using System;

namespace WebControlCenter.Services.Control
{
    public interface IStateCacheService
    {
        void Set(Guid id, object payload);
    }
}