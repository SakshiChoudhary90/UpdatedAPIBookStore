using apiOnlineBookStoreAdmin.Controllers;
using apiOnlineBookStoreProject.Models;
using coreBookStore.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace apiOnlineBookTesting
{
    public class AuthorTestController
    {
        private OnlineBookStoreAPIDbContext context;

        public static DbContextOptions<OnlineBookStoreAPIDbContext> dbContextOptions { get; set; }


        public static string connectionString = "Data Source=TRD-512;Initial Catalog=OnlineBookStoreAPIDbContext;Integrated Security=true;";

        static AuthorTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<OnlineBookStoreAPIDbContext>().UseSqlServer(connectionString).Options;
        }
        public AuthorTestController()
        {
            context = new OnlineBookStoreAPIDbContext(dbContextOptions);
        }

        [Fact]
        public async void Task_GetAuthorById_Return_OkResult()
        {
            var controller = new AuthorController(context);
            var AuthorId = 2;
            var data = await controller.Get(AuthorId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetAuthorById_Return_NotFound()
        {
            var controller = new AuthorController(context);
            var AuthorId = 6;
            var data = await controller.Get(AuthorId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetAuthorById_Return_MatchedData()
        {

            //Arrange
            var controller = new AuthorController(context);
            var AuthorId = 2;

            //Act

            var data = await controller.Get(AuthorId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var author = okResult.Value.Should().BeAssignableTo<Author>().Subject;
            Assert.Equal("Kate Bowler", author.AuthorName);
            Assert.Equal("123", author.AuthorImage);
        }
        [Fact]
        public async void TaskGetAuthorById_Return_BadRequestResult()
        {

            //Arrange
            var controller = new AuthorController(context);
            int? id = null;


            //Act

            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_Add_Author_Return_OkResult()
        {




            //Arrange
            var controller = new AuthorController(context);
            var author = new Author()
            {
                AuthorName = "Kate Bowler",
                AuthorDescription = "Wrote Autobiography",
                AuthorImage = "123"
            };

            //Act

            var data = await controller.Post(author);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);

        }

        [Fact]
        public async void Task_Add_Author_Return_BadRequest()
        {

            //Arrange
            var controller = new AuthorController(context);
            var author = new Author()
            {
                AuthorName = "Delhi Publisher",
                AuthorDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                AuthorImage = "123"
            };

            //Act

            var data = await controller.Post(author);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_Delete_Author_Return_OkResult()
        {

            //Arrange
            var controller = new AuthorController(context);
            var id = 1;

            //Act

            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);

        }

        [Fact]
        public async void Task_Delete_Author_Return_BadRequest()
        {

            //Arrange
            var controller = new AuthorController(context);
            int? id = null;

            //Act

            var data = await controller.Delete(id);

            //Assert

            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_Update_Author_Return_OkResult()
        {


            //Arrange
            var controller = new AuthorController(context);
            int id = 2;


            var author = new Author()
            {
                AuthorId = 2,
                AuthorName = "Kate Bowler",
                AuthorDescription = "Professor",
                AuthorImage = "123"
            };

            //Act

            var updateData = await controller.Put(id, author);

            //Assert
            Assert.IsType<OkObjectResult>(updateData);

        }

        [Fact]
        public async void Task_Update_Author_Return_BadRequest()
        {

            //Arrange
            var controller = new AuthorController(context);
            int? id = null;

            var author = new Author()
            {
                AuthorId = 12,
                AuthorName = "Delhi Publisher",
                AuthorDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                AuthorImage = "123"
            };

            //Act

            var data = await controller.Put(id, author);

            //Assert

            Assert.IsType<BadRequestResult>(data);

        }


        [Fact]
        public async void Task_Update_Author_Return_NotFound()
        {

            //Arrange
            var controller = new AuthorController(context);
            var AuthorId = 21;

            var author = new Author()
            {
                AuthorId = 1,
                AuthorName = "Delhi Publisher",
                AuthorDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                AuthorImage = "123"
            };

            //Act

            var data = await controller.Put(AuthorId, author);

            //Assert

            Assert.IsType<NotFoundResult>(data);
        }
    }
}
