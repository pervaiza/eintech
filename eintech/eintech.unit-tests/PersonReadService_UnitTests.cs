using eintech.api.Models;
using eintech.api.Repositories;
using eintech.api.Services;
using eintech.domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eintech.unit_tests
{
    public class PersonReadService_UnitTests
    {
        private Mock<IPersonRepository> _repository;
        private Guid TestId1 = Guid.Parse("8d5b57fe-8e17-4152-be99-7266958d0f8e");

        public PersonReadService_UnitTests()
        {
            _repository = new Mock<IPersonRepository>();
        }

        [Fact]
        public void PersonReadService_Get()
        {
            //Arrange
            var readService = new PersonReadService(_repository.Object);
            _repository.Setup(x => x.Get()).Returns(new List<Person>());

            //Act
            var restul = readService.Get();

            //Assert
            Assert.NotNull(restul);
        }

        [Fact]
        public async Task PersonReadService_Get_By_Id()
        {
            //Arrange
            var person = new Person()
            {
                Id = TestId1,
                FirstName = "Pervaiz",
                LastName = "Akhtar",
                Email = "m.akhtar@hotmail.co.uk",
                CreatedOn = DateTime.Now
            };
            var readService = new PersonReadService(_repository.Object);
            _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(person));

            //Act
            var restul = await readService.GetById(TestId1);

            //Assert
            Assert.NotNull(restul);
            Assert.Equal(restul.Id, TestId1);
        }

    }
}
