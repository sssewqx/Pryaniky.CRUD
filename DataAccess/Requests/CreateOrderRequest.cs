namespace Pryaniky.CRUD.DataAccess.Requests
{
    public class CreateOrderRequest
    {
        public List<Guid> ProductsId { get; set; } = new List<Guid>();
    }
}
