using System.Collections.Generic;
using WebControlCenter.CommandAdapter;

namespace WebControlCenter.Database.Services
{
    public interface IEntityRepository
    {
        void AddAdapter(IMqttAdapter adapter);
        void AddAdapters(List<IMqttAdapter> adapters);
    }
}