using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private IDataRepository<Categories, long> _iRepo;
        public CategoriesController(IDataRepository<Categories, long> repo)
        {
            _iRepo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable <Categories> Get()
        {
            return _iRepo.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Categories Get(int id)
        {
            return _iRepo.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] Categories category)
        {
            _iRepo.Add(category);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Categories category)
        {
            _iRepo.Update(category.CategoriesId, category);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _iRepo.Delete(id);
        }
    }
}
