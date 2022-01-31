using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public interface IUserFactory
    {
        List<User> CreateUsers(Dictionary<string, UiConfigurationRoot> configuration);
    }
}
