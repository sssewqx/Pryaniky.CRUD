using Pryaniky.CRUD.DataAccess.Entities;
using Pryaniky.CRUD.DataAccess.Requests;

namespace Pryaniky.CRUD.Abstractions
{
    public interface IProductService
    {
        Task<string> Create(CreateProductRequest product);
        Task<string> Update(UpdateProductRequest product);
        Task<string> Delete(Guid id);
        Task<ProductEntity> GetById(Guid id);
        Task<List<ProductEntity>> GetAll();
    }
}
