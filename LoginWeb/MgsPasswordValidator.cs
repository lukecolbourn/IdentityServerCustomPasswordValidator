using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginWeb
{
    public class CustomPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            int casinoId = int.Parse(context.Request.Raw["casinoid"]);

            if (context.Password == "123123")
            {
                if (context.UserName.StartsWith("luco"))
                {
                    context.Result = new GrantValidationResult(
                        subject: "818727",
                        authenticationMethod: "custom",
                        claims: new List<Claim>());

                    Console.WriteLine(context.Request.Raw);
                    return Task.FromResult(true);
                }
            }

            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "invalid custom credential");

            return Task.FromResult(false);
        }
    }
}
