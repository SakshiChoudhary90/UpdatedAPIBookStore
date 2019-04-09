
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
            var PublicationId = 11;
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
            var PublicationId = 11;

            //Act

            var data = await controller.Get(PublicationId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var publication = okResult.Value.Should().BeAssignableTo<Publication>().Subject;
            Assert.Equal("abcde", publication.PublicationName);
            Assert.Equal("df", publication.PublicationImage);
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
            var id = 14;
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
            var PublicationId = 15;

            var data = await controller.Get(PublicationId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var publication = okResult.Value.Should().BeAssignableTo<Publication>().Subject;
            var editpublication = new Publication()
            {
                PublicationId = publication.PublicationId,
                PublicationName = "Baba",
                PublicationDescription = publication.PublicationDescription,
                PublicationImage =publication.PublicationImage
            };
            var updateData = await controller.Put(PublicationId, editpublication);

            Assert.IsType<OkObjectResult>(updateData);
        }


        [Fact]
        public async void TaskPutPublicationById_Return_BadRequestResult()
        {

            //Arrange
            var controller = new PublicationController(context);
            int? id = null;


            //Act

            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

    }
}
