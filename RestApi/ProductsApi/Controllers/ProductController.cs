using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProductsApi.Controllers
{
    [Route("api/product/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> _service;

        public ProductController(IService<Product> service)
        {
            _service = service;
        }

        [HttpGet("")]
        [Authorize]
        public Object GetAll()
        {
            var data = _service.GetAll();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }

        [HttpGet("{id}")]
        [Authorize]
        public Object Get(int id)
        {
            var data = _service.Get(id);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public Object Create([FromBody] Product product)
        {
            try
            {
                _service.Create(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public bool Update(Product product)
        {
            try
            {
                _service.Update(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public bool Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
