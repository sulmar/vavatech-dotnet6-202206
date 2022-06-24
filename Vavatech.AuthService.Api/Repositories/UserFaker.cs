using Bogus;
using Microsoft.AspNetCore.Identity;
using Vavatech.AuthService.Api.Models;

namespace Vavatech.AuthService.Api.Repositories
{
    public class UserFaker : Faker<User>
    {
        public UserFaker(IPasswordHasher<User> passwordHasher)
        {
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.UserName, f => f.Person.UserName);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.HashedPassword, (f, user) => passwordHasher.HashPassword( user, "12345"));

            RuleFor(p => p.DateOfBirth, f => f.Date.Past(20));
        }
    }
}
