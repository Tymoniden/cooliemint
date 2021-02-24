using System;
using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public class UserFactory : IUserFactory
    {
        private readonly IPageFactory _pageFactory;

        public UserFactory(IPageFactory pageFactory)
        {
            _pageFactory = pageFactory ?? throw new ArgumentNullException(nameof(pageFactory));
        }

        public List<User> CreateUsers(Dictionary<string, UiConfigurationRoot> configuration)
        {
            var users = new List<User>();
            foreach (var configNode in configuration)
            {
                var user = new User()
                {
                    Name = configNode.Key,
                    Id = configNode.Value.Id.Value,
                    Pages = _pageFactory.CreatePages(configNode.Value.Categories)
                };

                users.Add(user);
            }

            return users;
        }
    }
}
