using Microsoft.AspNetCore.Mvc;
using Pryaniky.CRUD.Abstractions;
using Pryaniky.CRUD.DataAccess;
using Pryaniky.CRUD.DataAccess.Requests;
using Pryaniky.CRUD.DataAccess.Services;
using System.Text.Json;

namespace Pryaniky.CRUD.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;

        public OrderController(AppDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<string> CreateAsync([FromBody] JsonElement order)
        {
            try
            {
                var deserializedOrder = JsonSerializer
                    .Deserialize<List<Guid>>(order, 
                    new JsonSerializerOptions {
                            PropertyNameCaseInsensitive = true 
                        });

                   CreateOrderRequest request = new CreateOrderRequest();
                   request.ProductsId.AddRange(deserializedOrder);
                   var newOrder = await _orderService.Create(request);

                   return JsonSerializer.Serialize(newOrder); 
            } 
            catch 
            { 
                return $"Invalid order state: {order}"; 
            } 
        }
        [HttpPost] 
        public async Task<string> GetAll()
        {
            return JsonSerializer.Serialize(await _orderService.GetAll());
        }
        [HttpPost]
        public async Task<string> GetById([FromBody] JsonElement id)
        {
            try
            {
                var deserializedId = JsonSerializer.Deserialize<Guid>(id);
                return JsonSerializer.Serialize(await _orderService.GetById(deserializedId));
            }
            catch
            {
                return $"Invalid order state: {id}";
            }
        }
        [HttpPost]
        public async Task<string> Delete([FromBody] JsonElement id)
        {
            try
            {
                var deserializedId = JsonSerializer.Deserialize<Guid>(id);
                return JsonSerializer.Serialize(await _orderService.Delete(deserializedId));
            }
            catch
            {
                return $"Invalid order state: {id}";
            }
        }

    }
}
