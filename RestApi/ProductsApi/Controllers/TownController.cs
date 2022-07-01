using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProductsApi.Controllers
{
    [Route("api/towns/")]
    [ApiController]
    [Authorize]
    public class TownController : Controller
    {
        private readonly IDataProvider<Town> _dataProvider;

        public TownController(IDataProvider<Town> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var data = _dataProvider.GetAll();
            return Json(data);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var data = _dataProvider.Get(id);
            return Json(data);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Town town)
        {
            try
            {
                _dataProvider.Create(town);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPut("")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Town town)
        {
            try
            {
                _dataProvider.Update(town);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
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
