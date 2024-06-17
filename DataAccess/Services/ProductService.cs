using Microsoft.EntityFrameworkCore;
using Pryaniky.CRUD.Abstractions;
using Pryaniky.CRUD.DataAccess.Entities;
using Pryaniky.CRUD.DataAccess.Requests;

namespace Pryaniky.CRUD.DataAccess.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> Create(CreateProductRequest product) 
        {
             var productEntity = new ProductEntity() 
             {
                 Id = Guid.NewGuid(),
                 Name = product.Name,
                 Description = product.Description,
             };

             await _context.AddAsync(productEntity);
             await _context.SaveChangesAsync();

             return productEntity.Id.ToString();            
        }
        public async Task<string> Update(UpdateProductRequest product)
        {
            await _context.Products.Where(x => x.Id == product.Id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Name, p => product.Name == null ? p.Name : product.Name)
                .SetProperty(p => p.Description, p => product.Description == null ? p.Description : product.Description)
                .SetProperty(p => p.OrderId, p => product.OrderId == null ? p.OrderId : product.OrderId));
            //var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            //productToUpdate.OrderId = product.OrderId;
            await _context.SaveChangesAsync();

            return product.Id.ToString();
        }
        public async Task<string> Delete(Guid id)
        {
            await _context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return id.ToString();
        }
        public async Task<ProductEntity> GetById(Guid id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }
        public async Task<List<ProductEntity>> GetAll()
        {
            var productList = await _context.Products.AsNoTracking().ToListAsync();
            return productList;
        }
    }
}
