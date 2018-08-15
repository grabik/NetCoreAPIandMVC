using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoriesController (IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoriesDTO> dto = new List<CategoriesDTO>();

            var client = _clientFactory.CreateClient("prod");

            HttpResponseMessage res = await client.GetAsync("api/categories");
  
            if (res.IsSuccessStatusCode)
            {     
                var result = res.Content.ReadAsStringAsync().Result;
  
                dto = JsonConvert.DeserializeObject<List<CategoriesDTO>>(result);

            }    
            return View(dto);
        }

        // GET: Categories/Create  
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoriesId,Name,Description")] CategoriesDTO category)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("prod");

                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/categories", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        // GET:Categories/Edit/1  
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CategoriesDTO> dto = new List<CategoriesDTO>();
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = await client.GetAsync("api/categories");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CategoriesDTO>>(result);
            }

            var category = dto.SingleOrDefault(m => m.CategoriesId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/1  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("CategoriesId,Name,Description")] CategoriesDTO category)
        {
            if (id != category.CategoriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("prod");

                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync("api/categories", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        // GET: Categories/Delete/1  
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CategoriesDTO> dto = new List<CategoriesDTO>();
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = await client.GetAsync("api/categories");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CategoriesDTO>>(result);
            }

            var category = dto.SingleOrDefault(m => m.CategoriesId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = client.DeleteAsync($"api/categories/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
