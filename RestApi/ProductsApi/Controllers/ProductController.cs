using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Route("api/products/")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IDataProvider<Product> _dataProvider;

        public ProductController(IDataProvider<Product> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet("")]
        [Authorize(Roles = "User")]
        public IActionResult GetAll()
        {
            var data = _dataProvider.GetAll();
            return Json(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(Guid id)
        {
            var data = _dataProvider.Get(id);
            return Json(data);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Product product)
        {
            try
            {
                _dataProvider.Create(product);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Product product)
        {
            try
            {
                _dataProvider.Update(product);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _dataProvider.Delete(id);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
