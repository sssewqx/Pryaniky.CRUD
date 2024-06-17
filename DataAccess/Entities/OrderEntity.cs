namespace Pryaniky.CRUD.DataAccess.Entities
{
    public class OrderEntity
    {
       public Guid Id { get; set; }
       public DateTime CreatedAt { get; set; }
       public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
