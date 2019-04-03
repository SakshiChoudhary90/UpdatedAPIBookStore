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
    public class PublicationController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/publication").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Publication> data = JsonConvert.DeserializeObject<List<Publication>>(stringData);

            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Publication publication)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");

            string stringData = JsonConvert.SerializeObject(publication);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/publication",contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/publication/" + id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            Publication data = JsonConvert.DeserializeObject<Publication>(stringData);

           
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/publication/" + id).Result;
            
            string stringData = response.Content.ReadAsStringAsync().Result;
            Publication data = JsonConvert.DeserializeObject<Publication>(stringData);
            
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Publication publication)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(publication);



            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/publication/" + publication.PublicationId, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
           
        }

        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            HttpResponseMessage response = client.GetAsync("/api/publication/" + id).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;
            Publication data = JsonConvert.DeserializeObject<Publication>(stringData);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id,Publication publication)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61725");
            string stringData = JsonConvert.SerializeObject(publication);



            //var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync("/api/publication/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

    }
}