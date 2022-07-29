using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Route("api/cities/")]
    [ApiController]
    [Authorize]
    public class CityController : Controller
    {
        private readonly IDataProvider<City> _dataProvider;

        public CityController(IDataProvider<City> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var data = _dataProvider.GetAll();
            return Json(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var data = _dataProvider.Get(id);
            return Json(data);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] City city)
        {
            try
            {
                _dataProvider.Create(city);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(City city)
        {
            try
            {
                _dataProvider.Update(city);
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
