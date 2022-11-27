using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApplicationTest.Data;
using WebApplicationTest.Helpers;
using WebApplicationTest.Models;
using WebApplicationTest.Wrappers;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly DataService _dataService;

        private readonly IUriService uriService;

        public ItemController(IUriService uriService)
        {
            _dataService = DataService.GetDataService();
            this.uriService = uriService;
        }

        [HttpGet(Name = "GetItems")]
        public async Task<IActionResult> GetAll([FromQuery] Filter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new Filter(filter.PageNumber, filter.PageSize);
            var items = _dataService.GetItems().Where(i=>i.Category.Id == filter.CategoryId);

            var pagedData = items
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);
            var totalRecords = items.Count();

            var pagedReponse = PaginationHelper.CreatePagedReponse<Item>(pagedData.ToList(), validFilter, totalRecords, uriService, route);

            return Ok(pagedReponse);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var item = _dataService.GetItems().FirstOrDefault(i => i.Name == name);
            
            return Ok(new Response<Item>(item));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ItemModel item)
        {
            var createdItem = _dataService.AddItem(item);

            return Ok(new Response<Item>(createdItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ItemModel item)
        {
            var createdItem = _dataService.UpdateItem(item);

            return Ok(new Response<Item>(createdItem));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string itemName)
        {
            _dataService.DeleteItem(itemName);

            return Ok();
        }
    }
}
