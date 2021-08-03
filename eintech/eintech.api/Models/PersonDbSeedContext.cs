using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api.Models
{
    public class PersonDbSeedContext
    {
        public async Task SeedAsync(PersonDbContext context, ILogger<PersonDbSeedContext> logger)
        {
            if (!context.Persons.Any())
            {
                context.Persons.Add(new domain.Models.Person()
                {
                    FirstName = "Pervaiz",
                    LastName = "Akhtar",
                    Email = "m.akhtar@hotmail.co.uk",
                    CreatedOn = DateTime.Now
                });
                context.Persons.Add(new domain.Models.Person()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "smith@hotmail.co.uk",
                    CreatedOn = DateTime.Now
                });
                context.Persons.Add(new domain.Models.Person()
                {
                    FirstName = "James",
                    LastName = "Burns",
                    Email = "burns@hotmail.co.uk",
                    CreatedOn = DateTime.Now
                });
            }

            await context.SaveChangesAsync();
        }

    }
}
