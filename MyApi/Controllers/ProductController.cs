using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyApi.Services.ProductService;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }


        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetSigleProduct(int id)
        {
            var result = await _productService.GetSigleProduct(id);
            if (result == null)
                return NotFound("Product nor found");

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            var result = await _productService.AddProduct(product);
            return Ok(result);
        }


        [HttpPut("id")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product requets)
        {
            var result = await _productService.UpdateProduct(id, requets);
            if (result == null)
                return NotFound("Product nor found");

            return Ok(result);
        }


        [HttpDelete("id")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result == null)
                return NotFound("Product nor found");

            return Ok(result);
        }
    }
}
