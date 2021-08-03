using eintech.api.Models;
using eintech.api.Repositories;
using eintech.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eintech.unit_tests
{
    public class PersonRepository_UnitTests
    {
        public PersonDbContext _dbContext;
        private Guid TestId1 = Guid.Parse("cde3dd81-ea26-45b7-afc9-5eec13c80d08");

        [Fact]
        public async Task PersonRepository_Get()
        {
            //Arrange 
            Setup();

            //Act
            IPersonRepository repository = new PersonRepository(_dbContext);
            var people = await repository.Get().ToListAsync();

            //Assert
            Assert.NotNull(people);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task PersonRepository_Get_By_Id()
        {
            //Arrange 
            Setup();

            //Act
            IPersonRepository repository = new PersonRepository(_dbContext);
            var person = await repository.GetByIdAsync(TestId1);

            //Assert
            Assert.NotNull(person);
            Assert.Equal(person.Id, TestId1);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task PersonRepository_Create()
        {
            //Arrange 
            Setup();

            var newPerson = new domain.Models.Person()
            {
                FirstName = "UnitTest",
                LastName = "UnitTest",
                Email = "UnitTest@hotmail.co.uk",
                CreatedOn = DateTime.Now
            };

            //Act
            IPersonRepository repository = new PersonRepository(_dbContext);
            var person = await repository.Create(newPerson);

            //Assert
            Assert.NotNull(person);
            Assert.True(_dbContext.Persons.Count() == 4);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }


        [Fact]
        public async Task PersonRepository_Update()
        {
            //Arrange 
            Setup();

            var email = "testuser1@test.com";
            var personToUpdate = _dbContext.Persons.Find(TestId1);
            personToUpdate.Email = email;

            //Act
            IPersonRepository repository = new PersonRepository(_dbContext);
            var person = await repository.Update(personToUpdate);

            //Assert
            Assert.NotNull(person);
            Assert.Equal(person.Email, email);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        private void Setup()
        {

            var options = new DbContextOptionsBuilder<PersonDbContext>()
             .UseInMemoryDatabase(databaseName: "PersonDB").Options;
            _dbContext = new PersonDbContext(options);

            if (!_dbContext.Persons.Any())
            {
                _dbContext.Persons.Add(new Person()
                {
                    Id = TestId1,
                    FirstName = "Pervaiz",
                    LastName = "Akhtar",
                    Email = "m.akhtar@hotmail.co.uk",
                    CreatedOn = DateTime.Now
                });
                _dbContext.Persons.Add(new Person()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "smith@hotmail.co.uk",
                    CreatedOn = DateTime.Now
                });
                _dbContext.Persons.Add(new Person()
                {
                    FirstName = "James",
                    LastName = "Burns",
                    Email = "burns@hotmail.co.uk",
                    CreatedOn = DateTime.Now
                });

                _dbContext.SaveChanges();
            }
        }
    }
}
