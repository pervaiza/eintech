using eintech.api.Repositories;
using eintech.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api.Services
{
    public interface IPersonReadService
    {
        Task<List<Person>> Get();

        Task<Person> GetById(Guid id);
    }

    public class PersonReadService : IPersonReadService
    {
        private readonly IPersonRepository _personRepository;

        public PersonReadService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<Person>> Get()
        {
            return await _personRepository.Get().ToListAsync();
        }

        public async Task<Person> GetById(Guid id)
        {
            return await _personRepository.GetByIdAsync(id);
        }
    }
}
