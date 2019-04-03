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
    public class AuthorBiographyController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/authorbiography").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<AuthorBiography> data = JsonConvert.DeserializeObject<List<AuthorBiography>>(stringData);

            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(AuthorBiography author)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");

            string stringData = JsonConvert.SerializeObject(author);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/authorbiography", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }



        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/authorbiography/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            AuthorBiography data = JsonConvert.DeserializeObject<AuthorBiography>(stringData);
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/authorbiography/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            AuthorBiography data = JsonConvert.DeserializeObject<AuthorBiography>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(AuthorBiography authorBiography)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(authorBiography);



            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/authorbiography/" + authorBiography.AuthorBiographyId, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/authorbiography/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            AuthorBiography data = JsonConvert.DeserializeObject<AuthorBiography>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, AuthorBiography authorBiography)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(authorBiography);



            //var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync("/api/authorbiography/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

    }
}