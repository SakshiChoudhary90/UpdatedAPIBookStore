using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    public class BookCategoryController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/bookcategory").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<BookCategory> data = JsonConvert.DeserializeObject<List<BookCategory>>(stringData);

            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BookCategory bookCategory)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");

            string stringData = JsonConvert.SerializeObject(bookCategory);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/bookcategory", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/bookcategory/" + id).Result;


            string stringData = response.Content.ReadAsStringAsync().Result;

            BookCategory data = JsonConvert.DeserializeObject<BookCategory>(stringData);


            return View(data);
        }


        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/bookcategory/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            BookCategory data = JsonConvert.DeserializeObject<BookCategory>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(BookCategory bookCategory)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(bookCategory);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/bookcategory/" + bookCategory.BookCategoryId, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/bookcategory/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            BookCategory data = JsonConvert.DeserializeObject<BookCategory>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, BookCategory bookCategory)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(bookCategory);



            //var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync("/api/bookcategory/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

    }
}