using apiOnlineBookStoreProject.Controllers;
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
    public class CategoryTestController
    {
        private OnlineBookStoreAPIDbContext context;

        public static DbContextOptions<OnlineBookStoreAPIDbContext> dbContextOptions { get; set; }


        public static string connectionString = "Data Source=TRD-512;Initial Catalog=OnlineBookStoreAPIDbContext;Integrated Security=true;";

        static CategoryTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<OnlineBookStoreAPIDbContext>().UseSqlServer(connectionString).Options;
        }
        public CategoryTestController()
        {
            context = new OnlineBookStoreAPIDbContext(dbContextOptions);
        }

        [Fact]
        public async void Task_GetCategoryById_Return_OkResult()
        {
            var controller = new BookCategoryController(context);
            var BookCategoryId = 2;
            var data = await controller.Get(BookCategoryId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetCategoryById_Return_NotFound()
        {
            var controller = new BookCategoryController(context);
            var BookCategoryId = 6;
            var data = await controller.Get(BookCategoryId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetCategoryById_Return_MatchedData()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            var BookCategoryId = 1;

            //Act

            var data = await controller.Get(BookCategoryId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var bk = okResult.Value.Should().BeAssignableTo<BookCategory>().Subject;
            Assert.Equal("Fiction", bk.BookCategoryName);
            Assert.Equal("Fiction Desc", bk.BookCategoryDescription);
        }
        [Fact]
        public async void TaskGetCategoryById_Return_BadRequestResult()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            int? id = null;


            //Act

            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_Add_Category_Return_OkResult()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            var bk = new BookCategory()
            {
                BookCategoryName = "English",
                BookCategoryDescription = "English Desc",
                BookCategoryImage = "img333"
            };

            //Act

            var data = await controller.Post(bk);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);

        }

        [Fact]
        public async void Task_Add_Category_Return_BadRequest()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            var bk = new BookCategory()
            {
                BookCategoryName = "MathsMathsMathsMathsMathsMaths",
                BookCategoryDescription = "Maths Desc",
                BookCategoryImage = "pic123"
            };

            //Act

            var data = await controller.Post(bk);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }

        [Fact]
        public async void Task_DeleteCategory_Return_OkResult()
        {
            //Arrange
            var controller = new BookCategoryController(context);
            var id = 11;
            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<OkObjectResult>(data);

        }
        [Fact]
        public async void Task_DeleteCategory_Return_BadRequest()
        {
            //Arrange
            var controller = new BookCategoryController(context);
            int? id = null;
            //Act
            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Update_Category_Return_OkResult()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            var BookCategoryId = 11;

           //Assert
            var editBookCategory = new BookCategory()
            {
                BookCategoryId =11,
                BookCategoryName = "Baba",
                BookCategoryDescription = "Baba Desc",
                BookCategoryImage = "img123"
            };
            var updateData = await controller.Put(BookCategoryId, editBookCategory);

            Assert.IsType<OkObjectResult>(updateData);
        }


        [Fact]
        public async void TaskPutCategoryById_Return_BadRequestResult()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            int? id = null;


            //Act

            var data = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Update_Category_Return_NotFound()
        {

            //Arrange
            var controller = new BookCategoryController(context);
            var BookCategoryId =10;

            var bk = new BookCategory()
            {
                BookCategoryId = 1,
                BookCategoryName = "Delhi Publisher",
                BookCategoryDescription = "New Delhi Publishers is an International repute publisher with an orientation towards research, practical and Technical Applications.",
                BookCategoryImage = "123"
            };

            //Act

            var data = await controller.Put(BookCategoryId, bk);

            //Assert

            Assert.IsType<NotFoundResult>(data);
        }
    }
}
