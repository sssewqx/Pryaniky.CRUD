using Microsoft.EntityFrameworkCore;
using Pryaniky.CRUD.Abstractions;
using Pryaniky.CRUD.DataAccess.Entities;
using Pryaniky.CRUD.DataAccess.Requests;

namespace Pryaniky.CRUD.DataAccess.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        public OrderService(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<string> Create(CreateOrderRequest order)
        {
            var orderedProducts = _context.Products.Where(x => order.ProductsId.Contains(x.Id)).ToList();
            if(orderedProducts.Any(x => x.OrderId != null))
            {  
                var firstAlreadyOrdered = orderedProducts.First(x => x.OrderId !=  null);
                return $"U tryn to purhase already ordered product: {firstAlreadyOrdered.Id}.";
            }
            var orderEntity = new OrderEntity() 
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Products = orderedProducts,
            };

            await _context.AddAsync(orderEntity);   
            await _context.SaveChangesAsync();

            return orderEntity.Id.ToString(); 
        }
        public async Task<string> Delete(Guid id)
        {
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return id.ToString();
        }
        public async Task<OrderEntity> GetById(Guid id)
        {
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }
        public async Task<List<OrderEntity>> GetAll()
        {  
            var orderList = await _context.Orders.AsNoTracking().ToListAsync();
            return orderList;
        }
    }
}
