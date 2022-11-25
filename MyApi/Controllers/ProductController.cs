using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyApi.Services.ProductService;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary ="Get all products")]
        [ProducesResponseType(typeof(IList<Product>), 200)]
        [ActionName("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }

        
        [HttpGet("id")]
        [SwaggerOperation(Summary = "Get a product", Description = "Pass product id as parameter")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ActionName("GetSigleProduct")]
        public async Task<ActionResult<Product>> GetSigleProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.GetSigleProduct(id);
            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }

        
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new product", Description = "Pass the product as parameter")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("AddProduct")]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.AddProduct(product);
            return Ok(result);
        }

        
        [HttpPut("id")]
        [SwaggerOperation(Summary = "Update a product", Description = "Pass product id as parameter and update product")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ActionName("UpdateProduct")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product requets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.UpdateProduct(id, requets);
            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }
        
        
        [HttpDelete("id")]
        [SwaggerOperation(Summary = "Delete a product", Description = "Pass product id as parameter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.DeleteProduct(id);
            if (!result)
                return NotFound("Product not found");

            return Ok();
        }
        
    }
}
