using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WebApplicationTest.Data;
using WebApplicationTest.Models;
using WebApplicationTest.Wrappers;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly DataService _dataService;

        public CategoryController()
        {
            _dataService = DataService.GetDataService();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = _dataService.GetCategories();

            return Ok(new Response<List<Category>>(categories.ToList()));
        }
        
        [HttpGet("name")]
        public async Task<IActionResult> Get(string name)
        {
            var category = _dataService.GetCategories().FirstOrDefault(i => i.Name == name);

            return Ok(new Response<Category>(category));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var category = _dataService.AddCategory(name);

            return Ok(new Response<Category>(category));
        }
        

        [HttpPut]
        public async Task<IActionResult> Update(int id, string name)
        {
            var createdCategory = _dataService.UpdateCategory(id, name);

            return Ok(new Response<Category>(createdCategory));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            _dataService.DeleteCategory(name);

            return Ok();
        }
        
    }
}
