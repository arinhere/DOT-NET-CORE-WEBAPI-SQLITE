using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.products;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        // POST /api/product/add
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(AddProductDto dtoInstance){
            var productToAdd = new Product {
                Title = dtoInstance.Title,
                Description = dtoInstance.Description,
                IsActive = true,
                IsDeleted = false,
                CreatedOn = System.DateTime.Now,
                UserId = dtoInstance.UserId
            };

            _repo.Add(productToAdd);
            var returnData = await _repo.SaveAll();

            return Ok(new{
                StatusCode = 201,
                message = "Product Added"
            });
        }

        // GET /api/product/getProductsByUserId
        [HttpGet("getProductsByUserId/{userId}")]
        public async Task<IActionResult> GetAllProducts(int userId){
            var products = await _repo.ListProducts(userId);
            if(products != null)
                return Ok(products);
            
            return NotFound();            
        }

        // GET /api/product/get/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductById(int id) {
            var product = await _repo.GetProductById(id);
            if(product != null)
                return Ok(product);
            
            return NotFound();            
        }

    }

    
}
