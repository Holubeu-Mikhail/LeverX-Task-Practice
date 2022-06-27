using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Authorization;

namespace RestApi.Controllers
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
        public Object Create([FromBody] ProductType productType)
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
