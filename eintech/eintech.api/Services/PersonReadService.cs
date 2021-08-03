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
        List<Person> Get();

        Task<Person> GetById(Guid id);
    }

    public class PersonReadService : IPersonReadService
    {
        private readonly IPersonRepository _personRepository;

        public PersonReadService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public  List<Person> Get()
        {
            return _personRepository.Get().ToList();
        }

        public async Task<Person> GetById(Guid id)
        {
            return await _personRepository.GetByIdAsync(id);
        }
    }
}
