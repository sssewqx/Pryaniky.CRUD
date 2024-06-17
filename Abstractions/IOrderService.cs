using Pryaniky.CRUD.DataAccess.Entities;
using Pryaniky.CRUD.DataAccess.Requests;

namespace Pryaniky.CRUD.Abstractions
{
    public interface IOrderService
    {
        Task<string> Create(CreateOrderRequest order);
        Task<string> Delete(Guid id);
        Task<OrderEntity> GetById(Guid id);
        Task<List<OrderEntity>> GetAll();
    }
}
