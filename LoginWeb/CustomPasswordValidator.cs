using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginWeb
{
    public class CustomPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var loginResult = VerifyAccount(context);
            Console.WriteLine(context.Request.Raw);
            return Task.FromResult(loginResult);
        }

        private bool VerifyAccount(ResourceOwnerPasswordValidationContext context)
        {
            // Pull any other data off of the request
            int groupid = int.Parse(context.Request.Raw["groupid"]);

            // Call a 3rd party auth service here
            if (context.Password == "123123" && context.UserName.StartsWith("luco"))
            {
               context.Result = new GrantValidationResult(
                        subject: "818727",
                        authenticationMethod: "custom",
                        // Add claims from service call
                        claims: new List<Claim>());

                return true;
            }
            else
            {
                context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "invalid custom credential");
            }

            // Fail
            return false;
        }
    }
}
