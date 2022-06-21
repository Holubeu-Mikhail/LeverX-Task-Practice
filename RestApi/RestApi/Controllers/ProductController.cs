using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace RestApi.Controllers
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
