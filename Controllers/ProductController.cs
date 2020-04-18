using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.products;
using DOT_NET_CORE_WEBAPI_SQLITE.Helpers;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;
        public ProductController(IProductRepository repo, 
                    IOptions<CloudinarySettings> cloudinaryConfig,
                    IMapper mapper)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;

            // Setting up cloudinary to upload image
            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        // POST /api/product/add
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm]AddProductDto dtoInstance)
        {
            if(ModelState.IsValid){
                ImageUploadResult cloudinaryId = null;
                var productImage = dtoInstance.ProductImage;
                if(productImage.Length > 0){
                    using(var stream = productImage.OpenReadStream()){
                        var uploadParams = new ImageUploadParams(){
                            File = new FileDescription(productImage.Name, stream),
                            Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                        };

                        cloudinaryId = _cloudinary.Upload(uploadParams);
                    }
                } 
                
                var productToAdd = new Product
                {
                    Title = dtoInstance.Title,
                    Description = dtoInstance.Description,
                    IsActive = true,
                    IsDeleted = false,
                    CloudinaryId = cloudinaryId.PublicId,
                    CloudinaryUrl = cloudinaryId.Uri.ToString(),
                    CreatedOn = System.DateTime.Now,
                    UserId = dtoInstance.UserId
                };

                _repo.Add(productToAdd);
                var returnData = await _repo.SaveAll();

                return Ok(new
                {
                    StatusCode = 201,
                    Data = _mapper.Map<ProductResponseDto>(productToAdd)
                });
            } else {
                return BadRequest(new
                {
                    message = "Invalid Data Found"
                });
            }            
        }

        // GET /api/product/getProductsByUserId
        [HttpGet("getProductsByUserId/{userId}")]
        public async Task<IActionResult> GetAllProducts(int userId)
        {
            var products = await _repo.ListProducts(userId);
            if (products != null)
                return Ok(products);

            return NotFound();
        }

        // GET /api/product/get/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _repo.GetProductById(id);
            if (product != null)
                return Ok(product);

            return NotFound();
        }

    }


}
