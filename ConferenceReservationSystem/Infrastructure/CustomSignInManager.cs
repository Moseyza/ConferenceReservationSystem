using ConferenceReservationSystem.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Infrastructure
{
    public class CustomSignInManager : SignInManager<Person>
    {
        public CustomSignInManager(UserManager<Person> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<Person> claimsFactory,
           Microsoft.Extensions.Options.IOptions<IdentityOptions> optionsAccessor,
           Microsoft.Extensions.Logging.ILogger<SignInManager<Person>> logger,
            Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes,
           IUserConfirmation<Person> confirmation) :
            base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger,schemes, confirmation)
        {

        }


    }
}
