using eintech.api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api.Services
{
    public interface IPersonDeleteService
    {
        Task Delete(Guid id);
    }

    public class PersonDeleteService : IPersonDeleteService
    {
        private readonly IPersonRepository _personRepository;

        public PersonDeleteService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task Delete(Guid id)
        {
            await _personRepository.Delete(id);
        }
    }
}
