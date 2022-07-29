using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Route("api/product-types/")]
    [ApiController]
    [Authorize]
    public class ProductTypeController : Controller
    {
        private readonly IDataProvider<ProductType> _dataProvider;

        public ProductTypeController(IDataProvider<ProductType> dataProvider)
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
        public IActionResult Create([FromBody] ProductType productType)
        {
            try
            {
                _dataProvider.Create(productType);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ProductType productType)
        {
            try
            {
                _dataProvider.Update(productType);
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
