using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/author").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Author> data = JsonConvert.DeserializeObject<List<Author>>(stringData);

            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //    ViewBag.publications = new SelectList(context.Publications, "PublicationId", "PublicationName");
            //    ViewBag.categorys = new SelectList(context.Categorys, "CategoryId", "CategoryName");
            //    ViewBag.authors = new SelectList(context.Authors, "AuthorId", "AuthorName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Author author)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");

            string stringData = JsonConvert.SerializeObject(author);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/author", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }



        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/author/" + id).Result;
            
            string stringData = response.Content.ReadAsStringAsync().Result;
            Author data = JsonConvert.DeserializeObject<Author>(stringData);
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/author/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            Author data = JsonConvert.DeserializeObject<Author>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(author);



            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/author/" + author.AuthorId, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/author/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            Author data = JsonConvert.DeserializeObject<Author>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, Author author)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(author);



            //var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync("/api/author/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

    }
}