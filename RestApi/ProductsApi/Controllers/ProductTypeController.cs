using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProductsApi.Controllers
{
    [Route("api/product-type")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IService<ProductType> _service;

        public ProductTypeController(IService<ProductType> service)
        {
            _service = service;
        }

        [HttpGet("")]
        [Authorize]
        public string? GetAll()
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
        public string? Get(int id)
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
        public bool Create([FromBody] ProductType productType)
        {
            try
            {
                _service.Create(productType);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public bool Update(ProductType productType)
        {
            try
            {
                _service.Update(productType);
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
