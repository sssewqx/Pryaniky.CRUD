using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Pryaniky.CRUD.Abstractions;
using Pryaniky.CRUD.DataAccess;
using Pryaniky.CRUD.DataAccess.Entities;
using Pryaniky.CRUD.DataAccess.Requests;
using System.Text.Json;

namespace Pryaniky.CRUD.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        public ProductController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [HttpPost]
        public async Task<string> Create([FromBody] JsonElement product)
        {
            try
            {
                var deserializedProduct = JsonSerializer
                    .Deserialize<CreateProductRequest>(product, 
                    new JsonSerializerOptions {
                            PropertyNameCaseInsensitive = true 
                        });
                   var newProduct = await _productService.Create(deserializedProduct);
                   return JsonSerializer.Serialize(newProduct); 
            } 
            catch 
            { 
                return "Product state is not valid."; 
            }  
        }
        [HttpPost]
        public async Task<string> GetById([FromBody] JsonElement id)
        {
            try
            {
                var deserializedId = JsonSerializer.Deserialize<Guid>(id);
                return JsonSerializer.Serialize(await _productService.GetById(deserializedId));
            }
            catch
            {
                return $"Invalid product state: {id}";
            }
        }
        [HttpPost]
        public async Task<string> GetAll()
        {
            return JsonSerializer.Serialize(await _productService.GetAll());
        }
        [HttpPost]
        public async Task<string> Delete(JsonElement id)
        {
            try
            {
                var deserializedId = JsonSerializer.Deserialize<Guid>(id);
                return JsonSerializer.Serialize(await _productService.Delete(deserializedId));
            }
            catch
            {
                return $"Invalid product state: {id}";;
            }
        }

    }
}
