using Bogus;
using Vavatech.AuthService.Api.Models;

namespace Vavatech.AuthService.Api.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly IDictionary<int, User> _users;

        public FakeUserRepository(Faker<User> faker)
        {
            _users = faker.Generate(100).ToDictionary(p => p.Id);
        }


        public User? Get(string username) => _users.Values.SingleOrDefault(p => p.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
}
