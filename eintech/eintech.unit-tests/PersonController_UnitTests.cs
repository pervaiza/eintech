using eintech.api.Controllers;
using eintech.api.Models;
using eintech.api.Repositories;
using eintech.api.Services;
using eintech.domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eintech.unit_tests
{
    public class PersonController_UnitTests
    {
        private Mock<IPersonReadService> _readService;
        private Mock<IPersonDeleteService> _deleteService;
        private Mock<IPersonUpdateService> _updateService;

        public PersonController_UnitTests()
        {
            _readService = new Mock<IPersonReadService>();
            _deleteService = new Mock<IPersonDeleteService>();
            _updateService = new Mock<IPersonUpdateService>();
        }

        [Fact]
        public void PersonController_Get()
        {
            //Arrange
            PersonController _controller = new PersonController(_deleteService.Object, _readService.Object, _updateService.Object);
            _readService.Setup(x => x.Get()).Returns(new List<Person>());

            //Act
            var actionResult =  _controller.Get();
            var okReslut = actionResult as OkObjectResult;
            var okResultValue = (List<Person>)okReslut.Value;

            //Assert
            Assert.Equal(200, okReslut.StatusCode);
        }

        [Fact]
        public async Task PersonController_Get_By_Id()
        {
            //Arrange
            PersonController _controller = new PersonController(_deleteService.Object, _readService.Object, _updateService.Object);
            _readService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(new Person() { }));

            //Act
            var actionResult = await _controller.GetById(Guid.NewGuid());
            var okReslut = actionResult as OkObjectResult;
            var okResultValue = (Person)okReslut.Value;

            //Assert
            Assert.Equal(200, okReslut.StatusCode);
        }

        [Fact]
        public async Task PersonController_Create()
        {
            //Arrange
            Guid TestId1 = Guid.Parse("8d5b57fe-8e17-4152-be99-7266958d0f8e");
            var person = new Person()
            {
                Id = TestId1,
                FirstName = "Pervaiz",
                LastName = "Akhtar",
                Email = "m.akhtar@hotmail.co.uk",
                CreatedOn = DateTime.Now
            };
            PersonController _controller = new PersonController(_deleteService.Object, _readService.Object, _updateService.Object);
            _updateService.Setup(x => x.Create(It.IsAny<Person>())).Returns(Task.FromResult(new Person() { }));

            //Act
            var actionResult = await _controller.Create(person);
            var okReslut = actionResult as OkObjectResult;
            var okResultValue = (Person)okReslut.Value;

            //Assert
            Assert.Equal(200, okReslut.StatusCode);
        }

        [Fact]
        public async Task PersonController_Delete()
        {
            //Arrange
            Guid TestId1 = Guid.Parse("8d5b57fe-8e17-4152-be99-7266958d0f8e");
            
            PersonController _controller = new PersonController(_deleteService.Object, _readService.Object, _updateService.Object);
            _deleteService.Setup(x => x.Delete(It.IsAny<Guid>()));

            //Act
            var actionResult = await _controller.Delete(TestId1);
            var okReslut = actionResult as OkObjectResult;

            //Assert
            Assert.Equal(200, okReslut.StatusCode);
        }
    }
}
