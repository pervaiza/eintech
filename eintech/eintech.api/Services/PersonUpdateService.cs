using eintech.api.Repositories;
using eintech.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api.Services
{
    public interface IPersonUpdateService
    {
        Task<Person> Create(Person person);
    }

    public class PersonUpdateService : IPersonUpdateService
    {
        private readonly IPersonRepository _personRepository;

        public PersonUpdateService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> Create(Person person)
        {
            return await _personRepository.Create(person);
        }
    }
}
