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
    public class PersonUpdateService_UnitTests
    {
        private Mock<IPersonRepository> _repository;
        private Guid TestId1 = Guid.Parse("8d5b57fe-8e17-4152-be99-7266958d0f8e");

        public PersonUpdateService_UnitTests()
        {
            _repository = new Mock<IPersonRepository>();
        }

        [Fact]
        public async Task PersonUpdateService_Create()
        {
            //Arrange
            var newPerson = new Person()
            {
                Id = TestId1,
                FirstName = "Pervaiz",
                LastName = "Akhtar",
                Email = "m.akhtar@hotmail.co.uk",
                CreatedOn = DateTime.Now
            };

            var updateService = new PersonUpdateService(_repository.Object);

            _repository.Setup(x => x.Create(It.IsAny<Person>())).Returns(Task.FromResult(newPerson));

            var result = await updateService.Create(newPerson);

            Assert.Equal(result, newPerson);

            _repository.Verify(x => x.Create(newPerson), Times.Once);
        }
    }
}
