
using apiOnlineBookStoreAdmin.Controllers;
using apiOnlineBookStoreProject.Models;
using coreBookStore.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace apiOnlineBookTesting
{
    public class PublicationTestController
    {
        private OnlineBookStoreAPIDbContext context;

        public static DbContextOptions<OnlineBookStoreAPIDbContext> dbContextOptions { get; set; }


        public static string connectionString = "Data Source=TRD-512;Initial Catalog=OnlineBookStoreAPIDbContext;Integrated Security=true;";

        static PublicationTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<OnlineBookStoreAPIDbContext>().UseSqlServer(connectionString).Options;
        }
        public PublicationTestController()
        {
            context = new OnlineBookStoreAPIDbContext(dbContextOptions);
        }

        [Fact]
        public async void Task_GetPublicationById_Return_OkResult()
        {
            var controller = new PublicationController(context);
            var PublicationId = 15;
            var data = await controller.Get(PublicationId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetPublicationById_Return_NotFound()
        {
            var controller = new PublicationController(context);
            var PublicationId = 6;
            var data = await controller.Get(PublicationId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetPublicationById_Return_MatchedData()
        {

            //Arrange
            var controller = new PublicationController(context);
            var PublicationId = 15;

            //Act

            var data = await controller.Get(PublicationId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var publication = okResult.Value.Should().BeAssignableTo<Publication>().Subject;
            Assert.Equal("Baba2", publication.PublicationName);
            Assert.Equal("123", publication.PublicationImage);
        }
        [Fact]
        public async void TaskGetPublicationById_Return_BadRequestResult()
        {

            //Arrange
            var controller = new PublicationController(context);
            int? id = null;


            //Act

            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_Add_Publication_Return_OkResult()
        {

            //Arrange
            var controller = new PublicationController(context);
            var publication = new Publication()
            {
                PublicationName = "New",
                PublicationDescription = "desc",
                PublicationImage = "123"
            };

            //Act

            var data = await controller.Post(publication);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);

        }

        [Fact]
        public async void Task_Add_Publication_Return_BadRequest()
        {

            //Arrange
            var controller = new PublicationController(context);
            var publication = new Publication()
            {
                PublicationName = "Delhi New",
                PublicationDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                PublicationImage = "123"
            };

            //Act

            var data = await controller.Post(publication);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_DeletePublication_Return_OkResult()
        {
            //Arrange
            var controller = new PublicationController(context);
            var id = 17;
            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);

        }
        [Fact]
        public async void Task_DeletePublication_Return_BadRequest()
        {
            //Arrange
            var controller = new PublicationController(context);
            int? id = null;
            //Act
            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Update_Publication_Return_OkResult()
        {


            //Arrange
            var controller = new PublicationController(context);
            int id = 19;


            var pub = new Publication()
            {
                PublicationId = 19,
                PublicationName = "Kate",
                PublicationDescription = "Professor",
                PublicationImage = "123"
            };

            //Act

            var updateData = await controller.Put(id, pub);

            //Assert
            Assert.IsType<OkObjectResult>(updateData);

        }

        [Fact]
        public async void Task_Update_Publication_Return_BadRequest()
        {

            //Arrange
            var controller = new PublicationController(context);
            int? id = null;

            var publication = new Publication()
            {
                PublicationId = 15,
                PublicationName = "Delhi Publisher",
                PublicationDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                PublicationImage = "123"
            };

            //Act

            var data = await controller.Put(id, publication);

            //Assert

            Assert.IsType<BadRequestResult>(data);

        }


        [Fact]
        public async void Task_Update_Publication_Return_NotFound()
        {

            //Arrange
            var controller = new PublicationController(context);
            var PublicationId = 21;

            var author = new Publication()
            {
                PublicationId = 1,
                PublicationName = "Delhi Publisher",
                PublicationDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                PublicationImage = "123"
            };

            //Act

            var data = await controller.Put(PublicationId, author);

            //Assert

            Assert.IsType<NotFoundResult>(data);
        }

    }
}
