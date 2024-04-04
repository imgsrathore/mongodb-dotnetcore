using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbSampleApi.Entities;
using MongoDbSampleApi.Interfaces;

namespace MongoDbSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyProductController : ControllerBase
    {
        private readonly IProduct _product;
        public MyProductController(IProduct product)
        {
            _product = product;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var result = await _product.CreateProductAsync(product);
            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> ReadAllProduct()
        {
            var result = await _product.GetProductsAsync();
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(ObjectId id, [FromBody] Product product)
        {
            var result = await _product.UpdateProductAsync(id, product);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(ObjectId id)
        {
            var result = await _product.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}
