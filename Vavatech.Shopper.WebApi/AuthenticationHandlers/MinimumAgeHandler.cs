using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.WebApi.AuthenticationHandlers
{
    public static class MinimumAgeRequirmentExtensions
    {
        public static AuthorizationPolicyBuilder RequireMinimumAge(this AuthorizationPolicyBuilder builder, byte minimumAge)
        {
            return builder.AddRequirements(new MinimumAgeRequirment(minimumAge));
        }
    }

    public class MinimumAgeRequirment : IAuthorizationRequirement // marked interface 
    {
        public byte MinimumAge { get; }

        public MinimumAgeRequirment(byte minimumAge)
        {
            this.MinimumAge = minimumAge;
        }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirment requirement)
        {
            DateTime dateOfBirth = Convert.ToDateTime(context.User.FindFirstValue(ClaimTypes.DateOfBirth));

            byte age = (byte)(DateTime.Today.Year - dateOfBirth.Year);

            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;

        }
    }


    public class TheSameOwnerRequirment : IAuthorizationRequirement
    {
    }

    public class TheSameOwnerHandler : AuthorizationHandler<TheSameOwnerRequirment, Customer>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TheSameOwnerRequirment requirement, Customer resource)
        {
            string username = context.User.FindFirstValue(ClaimTypes.Name);

            if (resource.Owner.Username == username)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;

        }
    }




}
