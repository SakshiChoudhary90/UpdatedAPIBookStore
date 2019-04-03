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
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/book").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Book> data = JsonConvert.DeserializeObject<List<Book>>(stringData);

            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");

            string stringData = JsonConvert.SerializeObject(book);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/book", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/book/" + id).Result;


            string stringData = response.Content.ReadAsStringAsync().Result;

            Book data = JsonConvert.DeserializeObject<Book>(stringData);


            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/book/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            Book data = JsonConvert.DeserializeObject<Book>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(book);



            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/book/" + book.BookId, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/book/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            Book data = JsonConvert.DeserializeObject<Book>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, Book book)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(book);



            //var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync("/api/book/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

    }
}