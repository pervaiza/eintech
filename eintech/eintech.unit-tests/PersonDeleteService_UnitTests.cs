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
    public class PersonDeleteService_UnitTests
    {
        private Mock<IPersonRepository> _repository;
        private Guid TestId1 = Guid.Parse("8d5b57fe-8e17-4152-be99-7266958d0f8e");

        public PersonDeleteService_UnitTests()
        {
            _repository = new Mock<IPersonRepository>();
        }

        [Fact]
        public async Task PersonDeleteService_Delete()
        {
            //Arrange
            var deleteService = new PersonDeleteService(_repository.Object);

            _repository.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(new Person() { Id = TestId1 } ));

            await deleteService.Delete(TestId1);

            _repository.Verify(x => x.Delete(TestId1), Times.Once);
        }
    }
}
