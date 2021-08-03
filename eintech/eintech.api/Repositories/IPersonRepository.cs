using eintech.api.Models;
using eintech.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {

    }

    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly PersonDbContext _dbContext;

        public PersonRepository(PersonDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
