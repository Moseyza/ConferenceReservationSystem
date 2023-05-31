using ConferenceReservationSystem.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Infrastructure
{
    public class CustomUserManager : UserManager<Person>
    {
        public CustomUserManager(IUserStore<Person> store,
          IOptions<IdentityOptions> optionsAccessor,
          IHttpContextAccessor httpContextAccessor,
          IPasswordHasher<Person> passwordHasher,
          IEnumerable<IUserValidator<Person>> userValidators,
          IEnumerable<IPasswordValidator<Person>> passwordValidators,
          ILookupNormalizer keyNormalizer,
          IdentityErrorDescriber errors,
          IServiceProvider services,
          ILogger<UserManager<Person>> logger)
      : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public  async Task<Person> FindByNationalCodeAsync(string nationalCode)
        {
            var user = await Users.FirstOrDefaultAsync(x => x.NationalCode == nationalCode);
            return user;
        }
    }
}
